using UnityEngine;
using UnityEngine.UI;
using WorkflowUI.Scripts.Managers;
using WorkflowUI.Scripts.Model;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Behaviour
{
    public class EventBehaviour : MonoBehaviour
    {
        [Header("Interface")] 
        public GameObject ButtonsPanel;
        public Button DeleteEventButton;
        public Button AddEventButton;
        
        [Header("Run Time")] 
        [SerializeField] private Event _currentEvent;
        
        public void Initiate(Event @event)
        {
            _currentEvent = @event;
            
            DeleteEventButton.onClick.AddListener(OnDeleteButtonClick);
            AddEventButton.onClick.AddListener(OnEventButtonClick);
            
            ButtonsPanel.gameObject.SetActive(false);
        }

        private void OnDeleteButtonClick()
        {
            WorkflowManager.Instance.DeleteEvent(_currentEvent.Id);
        }

        private void OnEventButtonClick()
        {
            WorkflowManager.Instance.CreateNewEvent(_currentEvent);
        }

        public void OnEventClick()
        {
            ButtonsPanel.gameObject.SetActive(!ButtonsPanel.gameObject.activeInHierarchy);
        }
    }
}
