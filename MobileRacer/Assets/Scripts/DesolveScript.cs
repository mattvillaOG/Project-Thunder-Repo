using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolveScript : MonoBehaviour
{
    public bool clear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clear == true) { Destroy(this.gameObject); }
    }
}
