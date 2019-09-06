using System;
using UnityEngine;
using UnityEngine.UI;
using WorkflowUI.Scripts.Managers;
using Event = WorkflowUI.Scripts.Model.Event;

namespace WorkflowUI.Scripts.Views
{
    public class EventEditView : MonoBehaviour
    {
        public static EventEditView Instance { get; set; }

        public GameObject ViewGameObject;
        public Button CancelButton;
        public Toggle ProcessToggle;
        public Toggle NotificationToggle;

        [Space] 
        public Event CurrentEvent;
        private bool _startingView;
        
        private void Awake()
        {
            Instance = this;
            
            CancelButton.onClick.AddListener(OnCancelButtonClick);
            ProcessToggle.onValueChanged.AddListener(OnProcessToggleValueChange);
            NotificationToggle.onValueChanged.AddListener(OnNotificationToggleValueChange);
            
            CloseView();
        }

        public void OpenView(Event eventToEdit)
        {
            _startingView = true;

            CurrentEvent = eventToEdit;
            
            switch (eventToEdit.Type)
            {
                case Event.EventType.Undefined:
                    ProcessToggle.isOn = false;
                    NotificationToggle.isOn = false;
                    break;
                case Event.EventType.Process:
                    ProcessToggle.isOn = true;
                    NotificationToggle.isOn = false;
                    break;
                case Event.EventType.Notification:
                    ProcessToggle.isOn = false;
                    NotificationToggle.isOn = true;
                    break;
                default:
                    break;
            }
            
            ViewGameObject.SetActive(true);

            _startingView = false;
        }

        public void CloseView()
        {
            CurrentEvent = null;
            ViewGameObject.SetActive(false);
        }

        private void OnCancelButtonClick()
        {
            CloseView();
        }

        private void OnProcessToggleValueChange(bool value)
        {
            if (!value || _startingView)
                return;
            else
                if(CurrentEvent != null) CurrentEvent.EditEventType(Event.EventType.Process);
        }

        private void OnNotificationToggleValueChange(bool value)
        {
            if (!value || _startingView)
                return;
            else
                if(CurrentEvent != null) CurrentEvent.EditEventType(Event.EventType.Notification);
        }
    }
}
