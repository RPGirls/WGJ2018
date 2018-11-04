using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float minSpeedFirst = 0f;
    public float maxSpeedFirst = 0f;

    public float minSpeedSecond = 0f;
    public float maxSpeedSecond = 0f;

    private float speed = 0f;

    public Sprite[] objects;

    private bool canMove = false;
    private bool canBegin = false;
    private bool childUp = false;

    private float time = 0f;

    private void Start()
    {
        if (FindObjectOfType<SceneController>().GetComponent<SceneController>().isSecondRoom())
        {
            speed = Random.Range(minSpeedSecond, maxSpeedSecond);
        }
        else
        {
            speed = Random.Range(minSpeedFirst, maxSpeedFirst);
        }

    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if (canBegin)
        {
            if (childUp)
            {
                transform.Translate((-Vector3.up) * time, Space.World);
            }
            else
            {
                transform.Translate((-Vector3.right) * time, Space.World);
            }
        }
    }

    public void SetCanMove(bool can)
    {
        canMove = can;
    }

    public Sprite GiveSprite(int num)
    {
        return objects[num];
    }

    public void InitialSprites(float speed)
    {
        time = speed;
        canBegin = true;
        var coroutine = WaitToStop(0.8f);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitToStop(float time)
    {
        yield return new WaitForSeconds(time);
        canBegin = false;
    }

    public void SetChildUp(bool b)
    {
        childUp = b;
    }
}
