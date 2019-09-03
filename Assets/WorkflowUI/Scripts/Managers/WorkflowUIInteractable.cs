using UnityEngine;
using UnityEngine.UI;

namespace WorkflowUI.Scripts.Managers
{
    public class WorkflowUIInteractable : MonoBehaviour
    {
        [Header("Camera")]
        public Canvas CameraCanvas;
        
        [Header("Workflow Id")] 
        public Text WorkflowIdText;
        
        [Header("Options Panel")] 
        public GameObject OptionsPanelGO;
        public Button NewWorkflowButton;
        public Button NewEventButton;
        public Button UpdateLinesButton;

        [Header("Workflow Manager")] 
        public WorkflowManager WorkflowManager;

        [Header("Run Time")] 
        public bool OptionsPanelIsOpen;
        
        private void Awake()
        {
            if (!WorkflowManager)
                WorkflowManager = this.gameObject.GetComponent<WorkflowManager>();
            
            NewWorkflowButton.onClick.AddListener(OnNewWorkflowButtonClick);
            NewEventButton.onClick.AddListener(OnNewEventButtonClick);
            UpdateLinesButton.onClick.AddListener(OnUpdateLinesButtonClick);
            
            ToggleOptionsPanel(false);
        }

        private void OnNewWorkflowButtonClick()
        {
            WorkflowIdText.text = "Workflow: " + WorkflowManager.CreateNewWorkflow(true);
            ToggleOptionsPanel(false);
        }

        private void OnNewEventButtonClick()
        {
            WorkflowManager.CreateNewEvent(WorkflowManager.Instance.GetMousePositionOnCanvas());
            OnAnyOptionClicked();
        }

        private void OnUpdateLinesButtonClick()
        {
            WorkflowManager.UpdateLines();
            OnAnyOptionClicked();
        }

        public void OpenOptionsMenu()
        {
            OptionsPanelGO.transform.position = WorkflowManager.Instance.GetMousePositionOnCanvas();
            ToggleOptionsPanel(true);
        }

        public void CloseOptionsMenu()
        {
            if(OptionsPanelGO)
                ToggleOptionsPanel(false);
        }

        private void OnAnyOptionClicked()
        {
            ToggleOptionsPanel(false);
        }

        private void ToggleOptionsPanel(bool? openPanel)
        {
            var workflowActive = WorkflowManager.HasActiveWorkflow();
            
            NewEventButton.interactable = workflowActive;
            UpdateLinesButton.interactable = workflowActive;
            
            if (openPanel != null)
                OptionsPanelGO.gameObject.SetActive((bool) openPanel);
            else
                OptionsPanelGO.gameObject.SetActive(!OptionsPanelGO.gameObject.activeInHierarchy);

            OptionsPanelIsOpen = OptionsPanelGO.gameObject.activeInHierarchy;
        }
    }
}
