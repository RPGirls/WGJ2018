using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bubble;

    private bool bubbleOn = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bubbleOn)
            {
                bubble.SetActive(false);
            }
            else
            {
                bubble.SetActive(true);
            }

            bubbleOn = !bubbleOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "badObject")
        {
            Debug.Log("bad");
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "goodObject")
        {
            Debug.Log("good");
            Destroy(col.gameObject);
        }
    }
}
