using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "badObject")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "goodObject")
        {
            Destroy(col.gameObject);
        }
        FindObjectOfType<SceneController>().GetComponent<SceneController>().Counter();
    }
}
