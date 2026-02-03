using System.Collections;
using System.Threading;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{
    ManagerAction theManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       theManager = FindObjectOfType<ManagerAction>();
    }
    int RobsInt = 5;
    Touch touch;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.touches[0];
            float timer = 0.0f;
            bool hasMoved = false;
            bool hasTapped = false;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    print("The touch phase is" + touch.phase);
                    hasMoved = false;
                    timer = 0.0f;
                    break;
                case TouchPhase.Stationary:
                    print("The touch phase is" + touch.phase);
                    timer += Time.deltaTime;
                    break;
                case TouchPhase.Moved:
                    print("The touch phase is" + touch.phase);
                    hasMoved = true;
                    break;
                case TouchPhase.Ended:
                    print("The touch phase is" + touch.phase);
                    if (timer <= 1)
                    {
                        if (hasMoved == false)
                        {
                            print("That was a tap!");
                            hasTapped = true;
                            theManager.TapAt(touch.position);
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
