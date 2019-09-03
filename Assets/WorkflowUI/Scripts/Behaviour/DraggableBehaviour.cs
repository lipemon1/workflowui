using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorkflowUI.Scripts.Managers;

public class DraggableBehaviour : MonoBehaviour
{
    [Header("Drag Status")] 
    public bool CanDrag;

    public void StartDrag()
    {
        Debug.Log("Start Drag");
        CanDrag = true;
    }

    public void Drag()
    {
        Debug.Log("Dragging");
        if (CanDrag)
        {
            transform.position = WorkflowManager.Instance.GetMousePositionOnCanvas();
        }
    }

    public void StopDrag()
    {
        Debug.Log("Stop Drag");
        CanDrag = false;
    }
}
