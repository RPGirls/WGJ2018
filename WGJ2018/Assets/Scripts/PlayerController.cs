using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bubble;
    public GameObject tutorial;

    private bool bubbleOn = true;

    private bool canBubble = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canBubble)
        {
            if (bubbleOn)
            {
                bubble.SetActive(false);
            }
            else
            {
                bubble.SetActive(true);
            }

            if (tutorial.activeSelf)
            {
                tutorial.SetActive(false);
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
            FindObjectOfType<SceneController>().GetComponent<SceneController>().Counter(false);
        }
        if (col.gameObject.tag == "goodObject")
        {
            Debug.Log("good");
            Destroy(col.gameObject);
            FindObjectOfType<SceneController>().GetComponent<SceneController>().Counter(true);
        }
    }

    public void ChangeCanBubble(bool can)
    {
        canBubble = can;
    }
}
