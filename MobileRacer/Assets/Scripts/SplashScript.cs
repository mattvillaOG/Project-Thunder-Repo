using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    private Animation anim;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Play("SplashAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        //load the next sceen
        //
        if (done == true) { SceneManager.LoadScene(+1); }
    }
}
