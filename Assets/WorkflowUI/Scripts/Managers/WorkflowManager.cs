using System.Collections.Generic;
using UnityEngine;
using WorkflowUI.Scripts.Behaviour;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Managers
{
    public class WorkflowManager : MonoBehaviour
    {
        private static WorkflowManager Instance { get; set; }

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

        [ContextMenu("New Event")]
        public void CreateNewEvent()
        {
            var newEventBehaviour = Instantiate(EventBehaviourPrefab, WorkflowHolderPanel.transform);

            var newEvent = new Event(newEventBehaviour);
            newEventBehaviour.Initiate(newEvent);
            
            CurrentWorkflow.AddNewEvent(newEvent);
            
            //trying to connect with other event
            if (CurrentWorkflow.Events.Count > 1)
            {
                CreateNewLine(CurrentWorkflow.Events[CurrentWorkflow.Events.Count - 1], CurrentWorkflow.Events[CurrentWorkflow.Events.Count - 2]);
            }
        }

        [ContextMenu("New Line")]
        public void CreateNewLine(Event firstEvent, Event secondEvent)
        {
            var newLineBehaviour = Instantiate(LineBehaviourPrefab, WorkflowHolderPanel.transform);

            var eventsArray = new EventBehaviour[2];
            
            eventsArray[0] = firstEvent.EventBehaviour;
            eventsArray[1] = secondEvent.EventBehaviour;
            
            newLineBehaviour.Initiate(eventsArray);
            
            firstEvent.ConnectWithEvent(secondEvent.Id);
            secondEvent.ConnectWithEvent(firstEvent.Id);
            
            LineBehaviours.Add(newLineBehaviour);
        }

        [ContextMenu("Update Lines")]
        public void UpdateLines()
        {
            LineBehaviours.ForEach(lb => lb.UpdateLines());
        }
    }
}
