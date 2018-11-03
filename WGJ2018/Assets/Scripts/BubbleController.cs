using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "badObject")
        {
            Debug.Log("bad bubble");
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "goodObject")
        {
            Debug.Log("good bubble");
            Destroy(col.gameObject);
        }
    }
}
