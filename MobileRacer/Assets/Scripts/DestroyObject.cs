using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMe(destroyTimer));
    }

    // Update is called once per frame
    IEnumerator DestroyMe(float time)
    {
        yield return new WaitForSeconds(time);

        //cleans things up :)
        Destroy(gameObject);
    }
}
