using UnityEngine;

namespace WorkflowUI.Scripts.Behaviour
{
    public class LineBehaviour : MonoBehaviour
    {
        [Header("Inner Configuration")] 
        public LineRenderer LineRenderer;
        
        [Header("Run Time")]
        public EventBehaviour StartEvent;
        public EventBehaviour EndEvent;

        private void Update()
        {
            UpdateLines();
        }

        public void Initiate(EventBehaviour[] eventBehaviours)
        {
            StartEvent = eventBehaviours[0];
            EndEvent = eventBehaviours[1];
            
            DrawLine();
        }

        private void DrawLine()
        {
            LineRenderer.SetPosition(0, StartEvent.transform.position);
            LineRenderer.SetPosition(1, EndEvent.transform.position);
        }

        public void UpdateLines()
        {
            DrawLine();
        }
    }
}
