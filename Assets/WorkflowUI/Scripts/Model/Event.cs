using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WorkflowUI.Scripts.Behaviour;

namespace WorkflowUI.Scripts.Model
{
    [Serializable]
    public class Event
    {
        public enum EventType
        {
            Undefined = 0,
            Process = 1,
            Notification = 2
        }
        
        public string Id;
        public List<string> ConnectedEventIds = new List<string>();
        public EventBehaviour EventBehaviour;
        public EventType Type;

        public Event(EventBehaviour eventBehaviour)
        {
            Id = Guid.NewGuid().ToString();
            ConnectedEventIds = new List<string>();
            EventBehaviour = eventBehaviour;
        }

        public void ConnectWithEvent(string newEventId)
        {
            if(ConnectedEventIds.Contains(newEventId))
                Debug.Log("This event already is connected with this new event");
            else
            {
                ConnectedEventIds.Add(newEventId);
                EventBehaviour.AskForLines(Id, newEventId);
            }
        }

        public void EditEventType(EventType newType)
        {
            Type = newType;
            EventBehaviour.UpdateColor();
        }
    }
}
