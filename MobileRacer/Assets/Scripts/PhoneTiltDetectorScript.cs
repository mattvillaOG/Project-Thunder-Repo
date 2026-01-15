using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTiltDetector : MonoBehaviour
{

    [SerializeField] RacingMovement RM;

    private float tiltSensitivity = 0.1f;

    void Update()
    {        
        float tilt = Input.acceleration.x;
                
        if (tilt < -tiltSensitivity)
        {
            TurnLeft();
        }
        else
        {
            RM.movingLeft = false;
        }

        if (tilt > tiltSensitivity)
        {
            TurnRight();
        }
        else
        {
            RM.movingRight = false;
        }
    }

    void TurnLeft()
    {
        RM.movingLeft = true;
        Debug.Log("Turning Left!");
    }

    void TurnRight()
    {
        RM.movingRight = true;
        Debug.Log("Turning Right!");
    }
}
