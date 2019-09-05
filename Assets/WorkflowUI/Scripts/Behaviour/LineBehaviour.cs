using System;
using UnityEngine;
using WorkflowUI.Scripts.Managers;

namespace WorkflowUI.Scripts.Behaviour
{
    public class LineBehaviour : MonoBehaviour
    {
        [Header("Inner Configuration")] 
        public LineRenderer LineRenderer;

        [Header("Run Time")] 
        public string LineId;
        public EventBehaviour StartEvent;
        public EventBehaviour EndEvent;

        private void Update()
        {
            UpdateLines();
        }

        public void Initiate(EventBehaviour[] eventBehaviours, string lineId)
        {
            LineId = lineId;
            
            StartEvent = eventBehaviours[0];    
            EndEvent = eventBehaviours[1];
            
            DrawLine();
        }

        private void DrawLine()
        {
            if (StartEvent == null || EndEvent == null)
                Destroy(this.gameObject);
            else
            {
                try
                {
                    LineRenderer.SetPosition(0, StartEvent.transform.position);
                    LineRenderer.SetPosition(1, EndEvent.transform.position);
                }
                catch (Exception e)
                {
                    Debug.Log("Not possible to create the line renderer");
                }
            }
        }

        public void UpdateLines()
        {
            DrawLine();
        }

        private void OnDestroy()
        {
            WorkflowManager.Instance.DeleteLines();
        }
    }
}
