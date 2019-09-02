using System;
using System.Collections.Generic;
using UnityEditor;
using WorkflowUI.Scripts.Behaviour;

namespace WorkflowUI.Scripts.Model
{
    [Serializable]
    public class Event
    {
        public string Id;
        public List<string> ConnectedEventIds = new List<string>();
        public EventBehaviour EventBehaviour;

        public Event(EventBehaviour eventBehaviour)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedEventIds = new List<string>();
            EventBehaviour = eventBehaviour;
        }

        public void ConnectWithEvent(string newEventId)
        {
            ConnectedEventIds.Add(newEventId);
        }
    }
}
