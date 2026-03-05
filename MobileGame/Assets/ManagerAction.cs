using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerAction : MonoBehaviour
{
    public Transform selectedObject;                      // Currently selected object's transform
    public Renderer currentRenderer;                      // Renderer of the selected object
    public Material originalMaterial;                     // Original material so we can restore it
    public Plane dragPlane;                               // Plane used to calculate dragging movement
    public Vector3 dragOffset;                            // Offset between hit point and object position


    internal void TapAt(Vector2 position)
    {
        // Convert screen touch/click into a world-space ray
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If something was previously selected, restore its original material
            if (currentRenderer != null)
            {
                currentRenderer.material = originalMaterial;
                currentRenderer = null;
                originalMaterial = null;
                selectedObject = null;
            }
            // Mark this object as selected
            selectedObject = hit.transform;

            // Create a drag plane perpendicular to the camera, passing through the object
            dragPlane = new Plane(-Camera.main.transform.forward, selectedObject.position);

            float distance;
            // Find where the ray intersects the drag plane
            if (dragPlane.Raycast(ray, out distance))
            {
                // Calculate offset so dragging feels natural (no snapping)
                dragOffset = selectedObject.position - ray.GetPoint(distance);
            }
        }
        else
        {
            // If the ray hit nothing, clear selection
            if (currentRenderer != null)
            {
                currentRenderer.material = originalMaterial;
                currentRenderer = null;
                originalMaterial = null;
                selectedObject = null;
            }
        }
    }


    internal void moveIt(Vector2 position)
    {
        // Only move if something is selected
        if (selectedObject == null)
            return;

        // Convert screen position to a ray again
        Ray ray = Camera.main.ScreenPointToRay(position);

        float distance;
        // Check where this ray intersects the drag plane
        if (dragPlane.Raycast(ray, out distance))
        {
            // Move object to intersection point + original offset
            Vector3 point = ray.GetPoint(distance);
            selectedObject.position = point + dragOffset;
        }
    }


    internal void pinchAt(float startPos, float endPos)
    {
        
    }
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
