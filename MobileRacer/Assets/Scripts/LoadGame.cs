using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public GameObject instructions;
    public GameObject credits;


    private void Start()
    {
        //disable these menus on start
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    //this is the most exciting script ever
    public void BeginGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    //used to navgate instrucions
    public void OpenInstructions()
    {
        instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructions.SetActive(false);
    }

    //used to navgate creadits
    public void OpenCredits()
    {
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }
}
