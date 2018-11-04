using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "badObject")
        {
            gameObject.GetComponent<Animator>().SetTrigger("good");
            Destroy(col.gameObject);
            FindObjectOfType<SceneController>().GetComponent<SceneController>().Counter(true);
        }
        if (col.gameObject.tag == "goodObject")
        {
            gameObject.GetComponent<Animator>().SetTrigger("bad");
            Destroy(col.gameObject);
            FindObjectOfType<SceneController>().GetComponent<SceneController>().Counter(false);
        }
    }
}
