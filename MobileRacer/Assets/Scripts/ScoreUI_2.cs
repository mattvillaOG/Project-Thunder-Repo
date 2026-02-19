using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI_2 : MonoBehaviour
{

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            this.GetComponent<TextMeshProUGUI>().text = player.GetComponent<RacingMovement>().currentScore.ToString(); //get score text from player
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentScore.ToString(); //if no player, get from game manager
        }
    }
}
