using System;
using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    internal void TapAt(Vector2 position)
    {
        Ray ourRay = Camera.main.ScreenPointToRay(position);
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
        }
    }

    void ImSelected(GameObject selectedObject)
    {
        var objectRenderer = selectedObject.GetComponent<Renderer>();
        objectRenderer.material.SetColor("_Color", Color.yellow);
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
