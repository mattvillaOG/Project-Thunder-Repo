using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPowerUp : MonoBehaviour
{
    public bool mybool = false;

    // Update is called once per frame
    void Update()
    {
        if(mybool == true)
        {
            Debug.Log("triggered bool");
            GameObject.FindWithTag("Player").GetComponent<RacingMovement>().StartPowerUp();
        }
    }

}
