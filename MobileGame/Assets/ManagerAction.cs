using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerAction : MonoBehaviour
{
    [SerializeField] private Material selectionMaterial;   // Material used to highlight a selected object
    private Transform selectedObject;                      // Currently selected object's transform
    private Renderer currentRenderer;                      // Renderer of the selected object
    private Material originalMaterial;                     // Original material so we can restore it
    private Plane dragPlane;                               // Plane used to calculate dragging movement
    private Vector3 dragOffset;                            // Offset between hit point and object position


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

            // Try to get a renderer from the object we hit
            var renderer = hit.transform.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Store renderer and original material
                currentRenderer = renderer;
                originalMaterial = renderer.material;

                // Apply highlight material
                renderer.material = selectionMaterial;

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
        /*Ray ourRay = Camera.main.ScreenPointToRay(position);
        RaycastHit amIHitting;
        if (Physics.Raycast(ourRay.origin, ourRay.direction, out amIHitting))

        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.yellow, 2);
            Debug.Log("Did Hit");

        }
        else
        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.red, 2);
            Debug.Log("Did not Hit");
        }*/
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
