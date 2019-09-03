using System.Collections.Generic;
using UnityEngine;
using WorkflowUI.Scripts.Behaviour;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Managers
{
    public class WorkflowManager : MonoBehaviour
    {
        [Header("Canvas Reference")] 
        public Canvas WorkflowCanvas;
        public GameObject WorkflowHolderPanel;

        [Header("Prefabs References")] 
        public EventBehaviour EventBehaviourPrefab;
        public LineBehaviour LineBehaviourPrefab;

        [Header("Run Time")] 
        public Model.Workflow CurrentWorkflow;
        public List<LineBehaviour> LineBehaviours = new List<LineBehaviour>();

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
        
        public void CreateNewEvent(Vector3 eventSpawnPosition)
        {
            var newEventBehaviour = Instantiate(EventBehaviourPrefab, WorkflowHolderPanel.transform);
            newEventBehaviour.transform.position = eventSpawnPosition;

            var newEvent = new Event(newEventBehaviour);
            newEventBehaviour.Initiate(newEvent);
            
            CurrentWorkflow.AddNewEvent(newEvent);
            
            //trying to connect with other event
            if (CurrentWorkflow.Events.Count > 1)
            {
                CreateNewLine(CurrentWorkflow.Events[CurrentWorkflow.Events.Count - 1], CurrentWorkflow.Events[CurrentWorkflow.Events.Count - 2]);
            }
        }
        
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

        public bool HasActiveWorkflow()
        {
            return !string.IsNullOrEmpty(CurrentWorkflow.Id);
        }
    }
}
