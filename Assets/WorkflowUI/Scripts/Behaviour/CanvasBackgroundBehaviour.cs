using UnityEngine;
using UnityEngine.EventSystems;
using WorkflowUI.Scripts.Managers;

namespace WorkflowUI.Scripts.Behaviour
{
    public class CanvasBackgroundBehaviour : MonoBehaviour,IPointerClickHandler
    {
        [Header("Workflow UI Interactable")] 
        public WorkflowUIInteractable WorkflowUiInteractable;
 
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Middle)
                OnMiddleClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick(eventData);
        }

        private void OnLeftClick()
        {
            Debug.Log("Left click on Canvas Behaviour");
        }

        private void OnRightClick(PointerEventData ped)
        {
            Debug.Log("Right click on Canvas Behaviour");
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out Vector2 localCursor))
                return;
 
            Debug.Log("LocalCursor:" + localCursor);
            var finalVector = new Vector3(localCursor.x, localCursor.y, 0);
            WorkflowUiInteractable.OpenOptionsMenu(finalVector);
        }

        private void OnMiddleClick()
        {
            Debug.Log("Middle click on Canvas Behaviour");
        }
    }
}
