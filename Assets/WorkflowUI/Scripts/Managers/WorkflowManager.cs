using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorkflowUI.Scripts.Behaviour;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Managers
{
    public class WorkflowManager : MonoBehaviour
    {
        public static WorkflowManager Instance { get; set; }
        
        [Header("Canvas Reference")] 
        public Canvas WorkflowCanvas;
        public GameObject WorkflowHolderPanel;

        [Header("Prefabs References")] 
        public EventBehaviour EventBehaviourPrefab;
        public LineBehaviour LineBehaviourPrefab;

        [Header("COlors")] 
        public Color BaseColor;
        public Color ProcessColor;
        public Color NotificationColor;

        [Header("Run Time")] 
        public bool IsConnecting;
        public Model.Workflow CurrentWorkflow;
        public List<LineBehaviour> LineBehaviours = new List<LineBehaviour>();
        public Model.Event CurrentEventToConnect;

        private void Awake()
        {
            if (Instance)
                Destroy(this.gameObject);
            else
                Instance = this;
        }

        private void Update()
        {
            if(IsConnecting && Input.GetKeyDown(KeyCode.Escape))
                CancelConnectionWithEvent();
        }

        #region WORKFLOW

        [ContextMenu("New Workflow")]
        public void CreateNewWorkflow()
        {
            CurrentWorkflow = new Model.Workflow();
        }

        public string CreateNewWorkflow(bool withString)
        {
            CreateNewWorkflow();
            return CurrentWorkflow.Id;
        }
        
        public bool HasActiveWorkflow()
        {
            return !string.IsNullOrEmpty(CurrentWorkflow.Id);
        }

        #endregion

        #region EVENT
        public void CreateNewEvent(Event currentEvent = null)
        {
            var newEventBehaviour = Instantiate(EventBehaviourPrefab, WorkflowHolderPanel.transform);
            newEventBehaviour.transform.position = GetMousePositionOnCanvas();

            var newEvent = new Event(newEventBehaviour);
            newEventBehaviour.Initiate(newEvent);
            
            CurrentWorkflow.AddNewEvent(newEvent);
            
            //trying to connect with other event
            if (currentEvent != null)
                CreateNewLine(currentEvent, newEvent);
        }

        public void DeleteEvent(string id)
        {
            var eventToDelete = CurrentWorkflow.Events.FirstOrDefault(e => e.Id == id);

            if (eventToDelete == null) return;
            
            Destroy(eventToDelete.EventBehaviour.gameObject);
            
            CurrentWorkflow.Events.Remove(CurrentWorkflow.Events.FirstOrDefault(e => e.Id == id));

            foreach (var @event in CurrentWorkflow.Events.Where(e => e.ConnectedEventIds.Contains(id)))
                @event.ConnectedEventIds.Remove(id);
            
            UpdateLines();
        }

        public Color GetColorType(Event.EventType eventType)
        {
            switch (eventType)
            {
                case Event.EventType.Undefined:
                    return BaseColor;
                case Event.EventType.Process:
                    return ProcessColor;
                case Event.EventType.Notification:
                    return NotificationColor;
                default:
                    return BaseColor;
            }
        }

        #region Event Connection

        public void StartToConnectWithOtherEvent(Event baseEventToConnect)
        {
            SetIsConnecting(true);
            CurrentEventToConnect = baseEventToConnect;
        }

        public void ConnectWithEvent(Event eventToConnect)
        {
            CurrentEventToConnect.ConnectWithEvent(eventToConnect.Id);
            SetIsConnecting(false);
        }

        private void CancelConnectionWithEvent()
        {
            CurrentEventToConnect = null;
            SetIsConnecting(false);
        }

        private void SetIsConnecting(bool value)
        {
            IsConnecting = value;
            
            CurrentWorkflow.Events.ForEach(e => e.EventBehaviour.ChangeStatus(value ? EventBehaviour.EventBehaviourStatus.Connecting : EventBehaviour.EventBehaviourStatus.Idle));
        }

        #endregion
        
        #endregion

        #region Lines
        public void CreateNewLine(Event firstEvent, Event secondEvent)
        {
            var newLineBehaviour = Instantiate(LineBehaviourPrefab, WorkflowHolderPanel.transform);

            var eventsArray = new EventBehaviour[2];
            
            eventsArray[0] = firstEvent.EventBehaviour;
            eventsArray[1] = secondEvent.EventBehaviour;
            
            newLineBehaviour.Initiate(eventsArray, Guid.NewGuid().ToString());
            
            firstEvent.ConnectWithEvent(secondEvent.Id);
            secondEvent.ConnectWithEvent(firstEvent.Id);
            
            LineBehaviours.Add(newLineBehaviour);
        }

        public void CreateEventLines(string baseEventId, string newEventIdToConnectWith)
        {
            var firstEvent = CurrentWorkflow.Events.FirstOrDefault(e => e.Id == baseEventId);
            var secondEvent = CurrentWorkflow.Events.FirstOrDefault(e => e.Id == newEventIdToConnectWith);
            
            if(firstEvent != null && secondEvent != null)
                CreateNewLine(firstEvent, secondEvent);
        }

        [ContextMenu("Delete Lines")]
        public void DeleteLines()
        {
            LineBehaviours = LineBehaviours.Where(item => item != null).ToList();
        }

        [ContextMenu("Update Lines")]
        public void UpdateLines()
        {
            LineBehaviours.ForEach(lb => lb.UpdateLines());
        }
        
        #endregion
        
        public Vector3 GetMousePositionOnCanvas()
        {
            var canvas = Instance.WorkflowCanvas;
        
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = canvas.planeDistance;
            Camera renderCamera = canvas.worldCamera;
            return renderCamera.ScreenToWorldPoint(screenPos);
        }
    }
}
