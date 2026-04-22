using UnityEngine;

public class SphereScript : MonoBehaviour, I_Interactible
{
    private Renderer rend;
    private Material originalMaterial;

    [SerializeField] private Material highlightMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void TapAt(Vector3 worldPosition)
    {

    }

    public void MoveIt(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void PinchAt(float start, float end)
    {

    }

    public void ScaleIt(float scaleDelta)
    {
        transform.localScale += Vector3.one * scaleDelta;
    }

    public void RotateAt(float rotationDelta)
    {
        transform.Rotate(Vector3.up, rotationDelta, Space.World);
    }

    public void ObjectSelected()
    {
        if (highlightMaterial != null)
            rend.material = highlightMaterial;
    }

    public void ObjectDeselected()
    {
        rend.material = originalMaterial;
    }
}
