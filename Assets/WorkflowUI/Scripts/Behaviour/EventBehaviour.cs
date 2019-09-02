using UnityEngine;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Behaviour
{
    public class EventBehaviour : MonoBehaviour
    {
        [Header("Run Time")] 
        [SerializeField] private Event _currentEvent;
        
        public void Initiate(Event @event)
        {
            _currentEvent = @event;
        }
    }
}
