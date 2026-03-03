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
    float timer = 0.0f;
    bool hasMoved = false;
    bool hasTapped = false;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {
                Touch t1 = Input.touches[0];
                Touch t2 = Input.touches[1];
                float t_d_start = 0.0f;
                float t_d_new = 0.0f;
                float objectScaler;
                switch (t2.phase)
                {
                    case TouchPhase.Began:
                        print("The touch phase is" + t2.phase);
                        t_d_start = Vector2.Distance(t1.position, t2.position);
                        t_d_new = Vector2.Distance(t1.position, t2.position);
                        break;
                    case TouchPhase.Moved:
                        print("The touch phase is" + t2.phase);
                        t_d_new = Vector2.Distance(t1.position, t2.position);
                        break;
                    case TouchPhase.Ended:
                        print("The touch phase is" + t2.phase);
                        theManager.pinchAt(t_d_start, t_d_new);
                        break;
                }
            }
            else { 
            touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    print("The touch phase is" + touch.phase);
                    hasTapped = false;
                    hasMoved = false;
                    timer = 0.0f;
                    break;
                case TouchPhase.Stationary:
                    print("The touch phase is" + touch.phase);
                    timer += Time.deltaTime;
                    break;
                case TouchPhase.Moved:
                    print("The touch phase is" + touch.phase);
                        theManager.moveIt(touch.position);
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
        }
        else
        {
            
        }
    }
}
