using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLoop : MonoBehaviour
{
    [SerializeField] float loopPt; // how far to loop
    GameObject player;
    Vector3 startPos;
    int currentLoopX = 1;
    int currentLoopY = 1;
    int currentLoopZ = 1;

    // which direction to loop in?
    [SerializeField] bool xLoop = true;
    [SerializeField] bool yLoop = true;
    [SerializeField] bool zLoop = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        startPos = player.transform.position;
    }

    void Update()
    {
        LoopCheck();
    }

    void LoopCheck()
    {
        //i wrote this code in montreal for Sleep...

        //loop x
        if (player.transform.position.x > startPos.x + loopPt * currentLoopX && xLoop)
        {
            currentLoopX++;
            transform.position = new Vector3(transform.position.x + loopPt, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x < startPos.x + loopPt * (currentLoopX - 2) && xLoop)
        {
            currentLoopX--;
            transform.position = new Vector3(transform.position.x - loopPt, transform.position.y, transform.position.z);
        }

        //loop y
        if (player.transform.position.y > startPos.y + loopPt * currentLoopY && yLoop)
        {
            currentLoopY++;
            transform.position = new Vector3(transform.position.x, transform.position.y + loopPt, transform.position.z);
        }
        else if (player.transform.position.y < startPos.y + loopPt * (currentLoopY - 2) && yLoop)
        {
            currentLoopY--;
            transform.position = new Vector3(transform.position.x, transform.position.y - loopPt, transform.position.z);
        }

        //loop z
        if (player.transform.position.z > startPos.z + loopPt * currentLoopZ && zLoop)
        {
            currentLoopZ++;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + loopPt);
        }
        else if (player.transform.position.z < startPos.z + loopPt * (currentLoopZ - 2) && zLoop)
        {
            currentLoopZ--;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - loopPt);
        }
    }
}
