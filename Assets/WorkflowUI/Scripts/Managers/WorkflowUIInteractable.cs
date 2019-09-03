using UnityEngine;
using UnityEngine.UI;

namespace WorkflowUI.Scripts.Managers
{
    public class WorkflowUIInteractable : MonoBehaviour
    {
        [Header("Options Panel")] 
        public GameObject OptionsPanelGO;
        public Button NewWorkflowButton;
        public Button NewEventButton;
        public Button UpdateLinesButton;

        [Header("Workflow Manager")] 
        public WorkflowManager WorkflowManager;

        private void Awake()
        {
            if (!WorkflowManager)
                WorkflowManager = this.gameObject.GetComponent<WorkflowManager>();
            
            NewWorkflowButton.onClick.AddListener(OnNewWorkflowButtonClick);
            NewEventButton.onClick.AddListener(OnNewEventButtonClick);
            UpdateLinesButton.onClick.AddListener(OnUpdateLinesButtonClick);
        }

        private void OnNewWorkflowButtonClick()
        {
            WorkflowManager.CreateNewWorkflow();
            ToggleOptionsPanel(false);
        }

        private void OnNewEventButtonClick()
        {
            WorkflowManager.CreateNewEvent();
            OnAnyOptionClicked();
        }

        private void OnUpdateLinesButtonClick()
        {
            WorkflowManager.UpdateLines();
            OnAnyOptionClicked();
        }

        public void OpenOptionsMenu(Vector3 newPosition)
        {
            OptionsPanelGO.transform.position = newPosition;
            ToggleOptionsPanel(true);
        }

        private void OnAnyOptionClicked()
        {
            ToggleOptionsPanel(false);
        }

        private void ToggleOptionsPanel(bool? openPanel)
        {
            if (openPanel != null)
                OptionsPanelGO.gameObject.SetActive((bool) openPanel);
            else
                OptionsPanelGO.gameObject.SetActive(!OptionsPanelGO.gameObject.activeInHierarchy);
        }
    }
}
