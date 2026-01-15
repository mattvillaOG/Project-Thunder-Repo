using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
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
            this.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentHighScore.ToString(); //if no player, get from game manager
        }
    }
}
