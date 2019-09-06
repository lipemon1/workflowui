using System;
using UnityEngine;
using UnityEngine.UI;
using WorkflowUI.Scripts.Managers;
using WorkflowUI.Scripts.Model;
using WorkflowUI.Scripts.Views;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Behaviour
{
    public class EventBehaviour : MonoBehaviour
    {
        public enum EventBehaviourStatus
        {
            Idle,
            Connecting
        }

        public EventBehaviourStatus Status;
        
        [Header("Interface")] 
        public GameObject ButtonsPanel;
        public Button DeleteEventButton;
        public Button AddEventButton;
        public Button EditEventButton;
        public Button ConnectEventButton;

        [Space] 
        public Image EventImageBackground; 
        
        [Header("Run Time")] 
        [SerializeField] private Event _currentEvent;
        
        public void Initiate(Event @event)
        {
            _currentEvent = @event;
            
            DeleteEventButton.onClick.AddListener(OnDeleteButtonClick);
            AddEventButton.onClick.AddListener(OnAddEventButtonClick);
            EditEventButton.onClick.AddListener(OnEditEventButtonClick);
            ConnectEventButton.onClick.AddListener(OnConnectEventButtonClick);
            
            ButtonsPanel.gameObject.SetActive(false);
        }

        private void OnDeleteButtonClick()
        {
            WorkflowManager.Instance.DeleteEvent(_currentEvent.Id);
        }

        private void OnAddEventButtonClick()
        {
            WorkflowManager.Instance.CreateNewEvent(_currentEvent);
        }

        private void OnEditEventButtonClick()
        {
            EventEditView.Instance.OpenView(_currentEvent);
        }

        private void OnConnectEventButtonClick()
        {
            WorkflowManager.Instance.StartToConnectWithOtherEvent(_currentEvent);
        }

        public void OnEventClick()
        {
            switch (Status)
            {
                case EventBehaviourStatus.Idle:
                    ButtonsPanel.gameObject.SetActive(!ButtonsPanel.gameObject.activeInHierarchy);
                    break;
                case EventBehaviourStatus.Connecting:
                    WorkflowManager.Instance.ConnectWithEvent(_currentEvent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AskForLines(string baseId, string targetId)
        {
            WorkflowManager.Instance.CreateEventLines(baseId, targetId);
        }

        public void ChangeStatus(EventBehaviourStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateColor()
        {
            EventImageBackground.color = WorkflowManager.Instance.GetColorType(_currentEvent.Type);
        }
    }
}
