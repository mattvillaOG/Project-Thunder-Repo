using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuipPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    int quipCounter;

    //int quipCounter = player.gameObject.GetComponent<RacingMovement>().currentScore;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        int quipCounter = player.GetComponent<RacingMovement>().currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        int quipCounter = player.GetComponent<RacingMovement>().currentScore;

        //if (quipCounter == 100) { Debug.Log("100!"); }
        if (quipCounter == 1000) { Debug.Log("Bigger!"); }
        if (quipCounter == 5000) { Debug.Log("Amazing"); }
        if (quipCounter == 10000) { Debug.Log("Unstoppable!"); }
    }

    private void FixedUpdate()
    {
        if (quipCounter == 100) { Debug.Log("100!"); }
    }
}
