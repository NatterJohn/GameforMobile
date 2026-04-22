using System;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    [SerializeField] private Material selectionMaterial;

    private Transform selectedObject;

    private Plane dragPlane;
    private Vector3 dragOffset;
    private Vector2 lastPanPosition;
    private bool isPanning = false;
    private float delta = 0;

    private I_Interactible selectedInteractible;


    internal void tapAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        // Clear previous selection
        if (selectedObject != null)
        {
            selectedInteractible.ObjectDeselected();
            selectedObject = null;
            selectedInteractible = null;
        }

        if (Physics.Raycast(ray, out hit))
        {
            selectedInteractible = hit.transform.GetComponent<I_Interactible>();

            if (selectedInteractible != null)
            {
                selectedObject = hit.transform;

                selectedInteractible.ObjectSelected();

                dragPlane = new Plane(-Camera.main.transform.forward, selectedObject.position);

                float distance;
                if (dragPlane.Raycast(ray, out distance))
                {
                    dragOffset = selectedObject.position - ray.GetPoint(distance);
                }

                selectedInteractible.TapAt(selectedObject.position);
            }
        }
    }

    internal void moveIt(Vector2 position)
    {
        if (selectedObject == null || selectedInteractible == null)
        {
            if (!isPanning)
            {
                lastPanPosition = position;
                isPanning = true;
                return;
            }
            Vector2 delta = position - lastPanPosition;
            lastPanPosition = position;

            // Adjust sensitivity to taste
            float panSpeed = 0.01f;

            Vector3 move = new Vector3(-delta.x * panSpeed, -delta.y * panSpeed, 0);
            Camera.main.transform.Translate(move, Space.Self);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(position);

        float distance;
        if (dragPlane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 newPos = point + dragOffset;

            selectedObject.position = newPos;
            selectedInteractible.MoveIt(newPos);
        }
    }

    internal void pinchAt(float startPos, float endPos)
    {
        if (selectedInteractible == null)
        {
            delta = endPos - startPos;

            float zoomSpeed = 0.05f;

            Camera.main.transform.Translate(Vector3.forward * delta * zoomSpeed, Space.Self);
            return;
        }
        delta = endPos - startPos;

        // Scale
        selectedInteractible.ScaleIt(delta * 0.01f);

        // Rotate
        selectedInteractible.RotateAt(delta * 0.2f);

        // Notify pinch
        selectedInteractible.PinchAt(startPos, endPos);
    }
}
