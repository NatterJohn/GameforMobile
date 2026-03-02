using System;
using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    [SerializeField] private Material selectionMaterial;
    private Renderer currentRenderer = null;
    private Material originalMaterial = null;

    internal void TapAt(Vector2 position)
    {
        Ray ourRay = Camera.main.ScreenPointToRay(position);
        RaycastHit midasTouch;

        if (Physics.Raycast(ourRay, out midasTouch))
        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.yellow, 2);
            Debug.Log("Did Hit");
            // Restore previous selection
            if (currentRenderer != null)
            {
                currentRenderer.material = originalMaterial;
                currentRenderer = null;
                originalMaterial = null;
            }

            // Select new object
            var renderer = midasTouch.transform.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
                currentRenderer = renderer;
                renderer.material = selectionMaterial;
            }
        }
        else
        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.red, 2);
            Debug.Log("Did not Hit");
            // Tapped empty space → deselect
            if (currentRenderer != null)
            {
                currentRenderer.material = originalMaterial;
                currentRenderer = null;
                originalMaterial = null;
            }
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
    internal void moveit(float startPos, float endPos)
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
