using System;
using System.Collections.Generic;
using UnityEditor;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Model
{
    [Serializable]
    public class Workflow
    {
        public string Id;
        public List<Event> Events = new List<Event>();

        public Workflow()
        {
            Id = Guid.NewGuid().ToString();
            Events = new List<Event>();
        }

        public void AddNewEvent(Event @event)
        {
            Events.Add(@event);
        }
    }
}
