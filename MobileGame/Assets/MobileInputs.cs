using System.Collections;
using System.Threading;
using Unity.VisualScripting;
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
    Touch touch;          // Stores the current touch
    float timer = 0.0f;   // Tracks how long the finger stays on screen
    bool hasMoved = false; // Tracks whether the finger moved during the touch
    bool hasTapped = false; // Tracks whether a tap occurred
    float t_d_start = 0f;   // Initial distance between two fingers for pinch
    bool pinchStarted = false;
    private float lastTwistAngle = 0f; // Previous frame's twist angle
    private bool twisting = false;     // Whether a twist gesture is happening

    // Update is called once per frame
    void Update()
    {
        // Only process input if at least one finger is touching the screen
        if (Input.touchCount > 0)
        {
            // Only process input if two fingers are touching the screen
            if (Input.touchCount == 2)
            {
                Touch t1 = Input.touches[0];
                Touch t2 = Input.touches[1];
                float t_d_new = Vector2.Distance(t1.position, t2.position);
                float objectScaler;
                Vector2 p1 = t1.position;
                Vector2 p2 = t2.position;
                //Uses TAN of the touch positions to get the angle
                float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
                switch (t2.phase)
                {
                    case TouchPhase.Began:
                        // Store initial pinch distance and twist angle
                        t_d_start = Vector2.Distance(p1, p2);
                        lastTwistAngle = angle;
                        break;

                    case TouchPhase.Moved:
                        // Calculate twist rotation since last frame
                        float twistDelta = Mathf.DeltaAngle(lastTwistAngle, angle);
                        lastTwistAngle = angle;
                        // Calculate new pinch distance
                        t_d_new = Vector2.Distance(p1, p2);
                        // Send pinch (zoom)
                        theManager.pinchAt(t_d_start, t_d_new);
                        // Send twist rotation
                        theManager.twistAt(twistDelta);
                        break;

                    case TouchPhase.Ended:
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
                    // If the screen was touched for less than 1 second and the finger didn't move, it's a tap
                    if (timer <= 1)
                    {
                        if (hasMoved == false)
                        {
                            print("That was a tap!");
                            hasTapped = true;
                            theManager.tapAt(touch.position);
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
