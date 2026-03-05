using UnityEngine;

public class SphereScript : MonoBehaviour, I_Interactible
{
    [SerializeField] private Material selectionMaterial;   // Material used to highlight a selected object
    private Transform selectedObject;                      // Currently selected object's transform
    private Renderer currentRenderer;                      // Renderer of the selected object
    private Material originalMaterial;                     // Original material so we can restore it

    public void moveIt(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public void TapAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Try to get a renderer from the object we hit
            var renderer = hit.transform.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Store renderer and original material
                currentRenderer = renderer;
                originalMaterial = renderer.material;

                // Apply highlight material
                renderer.material = selectionMaterial;
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
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
