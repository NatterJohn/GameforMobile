using System.Collections;
using System.Threading;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    int RobsInt = 5;
    Touch RobsTouch;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            RobsTouch = Input.touches[0];
            float timer = 0.0f;
            bool hasMoved = false;
            switch (RobsTouch.phase)
            {
                case TouchPhase.Began:
                    //print("The touch phase is" + RobsTouch.phase);
                    hasMoved = false;
                    timer = 0.0f;
                    break;
                case TouchPhase.Stationary:
                    //print("The touch phase is" + RobsTouch.phase);
                    timer += Time.deltaTime;
                    break;
                case TouchPhase.Moved:
                    //print("The touch phase is" + RobsTouch.phase);
                    hasMoved = true;
                    break;
                case TouchPhase.Ended:
                    //print("The touch phase is" + RobsTouch.phase);
                    if (timer <= 1)
                    {
                        if (hasMoved == false)
                        {
                            print("That was a tap!");
                            print(timer);
                        }
                    }
                    break;
            }
        }
        else
        {
            print(RobsInt);
        }
    }
}
