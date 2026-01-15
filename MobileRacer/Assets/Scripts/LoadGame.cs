using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    //this is the most exciting script ever
    public void BeginGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
