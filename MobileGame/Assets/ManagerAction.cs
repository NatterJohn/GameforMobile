using System;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    [SerializeField] private Material selectionMaterial;


    private Transform selectedObject;          // The currently selected object in the scene
    private Plane dragPlane;                   // Plane used for dragging objects in world space
    private Vector3 dragOffset;                // Offset between touch point and object position
    private Vector2 lastPanPosition;           // Last finger position for camera panning
    private bool isPanning = false;            // True while the user is dragging the camera
    private float pinchPos = 0;                // Stores pinch delta for zooming or scaling

    private I_Interactible selectedInteractible; // Interface for interactible objects


    internal void tapAt(Vector2 position)
    {
        // Convert screen touch position into a world-space ray
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        // If something was previously selected, deselect it
        if (selectedObject != null)
        {
            selectedInteractible.ObjectDeselected();
            selectedObject = null;
            selectedInteractible = null;
        }
        // Raycast to see if we hit something new
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object implements I_Interactible
            selectedInteractible = hit.transform.GetComponent<I_Interactible>();

            if (selectedInteractible != null)
            {
                selectedObject = hit.transform;
                // Notify object that it is selected
                selectedInteractible.ObjectSelected();
                // Create a drag plane perpendicular to the camera to prevent dragging objects closer or farther from the camera
                dragPlane = new Plane(-Camera.main.transform.forward, selectedObject.position);
                // Calculate drag offset so object doesn't jump to the touch position when we start dragging
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
        // If nothing is selected, we pan the camera instead
        if (selectedObject == null || selectedInteractible == null)
        {
            // First frame of panning — store initial finger position
            if (!isPanning)
            {
                lastPanPosition = position;
                isPanning = true;
                return;
            }

            Vector2 panMovement = position - lastPanPosition;
            lastPanPosition = position;

            float panSpeed = 0.01f;
            // Move camera opposite to finger movement (e.g. dragging down moves camera up)
            Vector3 move = new Vector3(-panMovement.x * panSpeed, -panMovement.y * panSpeed, 0);
            Camera.main.transform.Translate(move, Space.Self);
            return;
        }
        // If an object is selected, we move it based on the drag plane
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
        print("That was a pinch!");
        // If nothing is selected, pinch zooms the camera
        if (selectedInteractible == null)
        {
            pinchPos = endPos - startPos;

            float zoomSpeed = 0.5f;
            float minZoom = -5f;
            float maxZoom = -30f;

            Camera.main.transform.Translate(Vector3.forward * pinchPos * zoomSpeed, Space.Self);
            Vector3 pos = Camera.main.transform.localPosition;
            // Clamp zoom distance to prevnt camera going too far in or out
            pos.z = Mathf.Clamp(pos.z, maxZoom, minZoom);
            Camera.main.transform.localPosition = pos;
            return;
        }
        // If an object is selected, we scale and rotate it based on the pinch
        pinchPos = endPos - startPos;

        selectedInteractible.ScaleIt(pinchPos * 0.00005f);
        selectedInteractible.RotateAt(pinchPos * 0.5f);
        selectedInteractible.PinchAt(startPos, endPos);
    }

    internal void twistAt(float twistDelta)
    {
        // If nothing is selected, twist rotates the camera
        if (selectedInteractible == null)
        {
            // Rotate camera
            Camera.main.transform.Rotate(Vector3.forward, -twistDelta, Space.Self);
            return;
        }
        // If an object is selected, we rotate it based on the twist
        // Rotate selected object
        selectedInteractible.RotateAt(twistDelta);
    }
}
