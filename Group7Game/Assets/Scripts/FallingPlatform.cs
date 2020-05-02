using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float fallDelay;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player")) //if player touches platform
        {
            StartCoroutine(fall());
        }
    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(fallDelay); //wait specified number of seconds before platform falls
        rb2d.isKinematic = false;
        //GetComponent<Collider2D>().isTrigger = true;
        yield return 0;

    }
}