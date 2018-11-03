using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Sprite[] objects;

    private bool canMove = false;

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
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
}
