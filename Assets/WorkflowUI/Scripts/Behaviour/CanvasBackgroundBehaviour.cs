using UnityEngine;
using UnityEngine.EventSystems;
using WorkflowUI.Scripts.Managers;

namespace WorkflowUI.Scripts.Behaviour
{
    public class CanvasBackgroundBehaviour : MonoBehaviour,IPointerClickHandler
    {
        [Header("Configuration")] 
        public WorkflowUIInteractable WorkflowUiInteractable;
 
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Middle)
                OnMiddleClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick();
        }

        private void OnLeftClick()
        {
            Debug.Log("Left click on Canvas Behaviour");
            WorkflowUiInteractable.CloseOptionsMenu();
        }

        private void OnRightClick()
        {
            Debug.Log("Right click on Canvas Behaviour");
            WorkflowUiInteractable.OpenOptionsMenu();
        }

        private void OnMiddleClick()
        {
            Debug.Log("Middle click on Canvas Behaviour");
        }
    }
}
