using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSkyAnim : MonoBehaviour
{
    Animator skyAnimator; //this is the animator that controls the skay anim transitions

    //this is a bool used to communicate with the other script
    public bool speeding;

    // Start is called before the first frame update
    void Start()
    {
        //this gets the animator from the gameobject this is attached to, which should be the sky background
        skyAnimator = this.GetComponent<Animator>();
        //this tells the animator that the animation perameter bool is set to false, becasue the player is not powerd up when they start.
        speeding = false;
        skyAnimator.SetBool("speeding", false);
    }

    // Update is called once per frame
    void Update()
    {
        //when this script finds out that our player bool is true, then we can set the animaiotn bool to true as well, so that when the plaeyr is powered up, the animation is alwo going to play.
        if (speeding == true) {skyAnimator.SetBool("speeding", true);}
        //this turns it off, doing the opposite of the line above. 
        if (speeding == false) {skyAnimator.SetBool("speeding", false); }
    }
}
