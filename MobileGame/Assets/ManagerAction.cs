using UnityEngine;

public class ManagerAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ray ourRay = Camera.main.ScreenPointToRay(transform.position);
        Debug.DrawRay(ourRay.origin, ourRay.origin+100*ourRay.direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
