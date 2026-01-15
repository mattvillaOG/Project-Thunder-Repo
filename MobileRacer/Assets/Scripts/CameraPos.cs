using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    GameObject player; // track player position
    public int yDist; // how far above the player
    public int zDist; // how far behind the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //lock to player position
        gameObject.transform.position = new Vector3(0, player.transform.position.y - yDist, player.transform.position.z - zDist);
    }
}
