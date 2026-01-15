using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    //taken from https://gamedevbeginner.com/billboards-in-unity-and-how-to-make-your-own/

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        //Vector3 adjustedCameraPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        //transform.LookAt(adjustedCameraPos);
        //transform.Rotate(0, 180, 0);
    }
}
