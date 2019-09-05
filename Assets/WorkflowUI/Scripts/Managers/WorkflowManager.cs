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

        [Header("Run Time")] 
        public Model.Workflow CurrentWorkflow;
        public List<LineBehaviour> LineBehaviours = new List<LineBehaviour>();

        private void Awake()
        {
            if (Instance)
                Destroy(this.gameObject);
            else
                Instance = this;
        }

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
            
            UpdateLines();
        }
        
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

        public bool HasActiveWorkflow()
        {
            return !string.IsNullOrEmpty(CurrentWorkflow.Id);
        }
        
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
