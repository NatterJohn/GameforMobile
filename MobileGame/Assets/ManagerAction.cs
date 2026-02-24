using System;
using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    [SerializeField] private Material selectionMaterial;
    private Material deselectedMaterial;
    internal void TapAt(Vector2 position)
    {
        Ray ourRay = Camera.main.ScreenPointToRay(position);
        RaycastHit amIHitting;
        if (Physics.Raycast(ourRay.origin, ourRay.direction, out amIHitting))

        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.yellow, 2);
            Debug.Log("Did Hit");
            var selectedObject = amIHitting.transform;
            var selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
            if (selectedObjectRenderer != null)
            {
                selectedObjectRenderer.material = selectionMaterial;
            }
        }
        else
        {
            Debug.DrawRay(ourRay.origin, ourRay.origin + 100 * ourRay.direction, Color.red, 2);
            Debug.Log("Did not Hit");
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
