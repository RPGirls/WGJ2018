using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float minTimeToSpawnFirst;
    public float maxTimeToSpawnFirst;
    public float minTimeToSpawnSecond;
    public float maxTimeToSpawnSecond;
    public GameObject badObject;
    public GameObject goodObject;
    public float initialObjectsSpeed = 0.5f;

    private List<int> spawnNumber;

    private void Start()
    {
        spawnNumber = new List<int>();
    }

    public void ControlEnemies(bool canSpawn)
    {
        if (canSpawn)
        {
            SpawnEnemy();
            StartCoroutine("EnemySpawner");
        }
        else
        {
            StopCoroutine("EnemySpawner");
        }
    }

    IEnumerator EnemySpawner()
    {
        float time;
        if (FindObjectOfType<SceneController>().GetComponent<SceneController>().isSecondRoom())
        {
            time = Random.Range(minTimeToSpawnSecond, maxTimeToSpawnSecond);
        } else
        {
            time = Random.Range(minTimeToSpawnFirst, maxTimeToSpawnFirst);
        }
            
        yield return new WaitForSeconds(time);
        SpawnEnemy();
        StartCoroutine("EnemySpawner");
    }

    private void SpawnEnemy()
    {
        if (Random.Range(0, 2) == 0)
        {
            int n = (int)(Random.Range(0, (float)transform.childCount));
            var obj = Instantiate(badObject, new Vector3(transform.GetChild(n).transform.position.x, transform.GetChild(n).transform.position.y, 0), Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<EnemyController>().GiveSprite((int)Random.Range(0, 2));
            obj.transform.SetParent(transform.parent);
            obj.GetComponent<EnemyController>().SetCanMove(true);
        }
        else
        {
            int n02 = (int)(Random.Range(0, (float)transform.childCount));
            var obj02 = Instantiate(goodObject, new Vector3(transform.GetChild(n02).transform.position.x, transform.GetChild(n02).transform.position.y, 0), Quaternion.identity);
            obj02.GetComponent<SpriteRenderer>().sprite = obj02.GetComponent<EnemyController>().GiveSprite((int)Random.Range(0, 2));
            obj02.transform.SetParent(transform.parent);
            obj02.GetComponent<EnemyController>().SetCanMove(true);
        }
    }

    public void SpawnInitial()
    {
        for (int i = 0; i < 3; i++)
        {
            var fatherCount = RandomChoose();
            if (fatherCount >= 0 && fatherCount <= 2)
            {
                var obj = Instantiate(badObject, new Vector3(transform.GetChild(fatherCount).transform.position.x, transform.GetChild(fatherCount).transform.position.y, 0), Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<EnemyController>().GiveSprite(i);
                obj.transform.SetParent(transform.parent);
                obj.GetComponent<EnemyController>().InitialSprites(initialObjectsSpeed);
                obj.GetComponent<EnemyController>().SetChildUp(true);
            }
            else
            {
                var obj = Instantiate(badObject, new Vector3(transform.GetChild(fatherCount).transform.position.x, transform.GetChild(fatherCount).transform.position.y, 0), Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<EnemyController>().GiveSprite(i);
                obj.transform.SetParent(transform.parent);
                obj.GetComponent<EnemyController>().InitialSprites(initialObjectsSpeed);
            }
        }

        for (int x = 0; x < 3; x++)
        {
            var fatherCount02 = RandomChoose();
            if (fatherCount02 >= 0 && fatherCount02 <= 2)
            {
                var obj02 = Instantiate(goodObject, new Vector3(transform.GetChild(fatherCount02).transform.position.x, transform.GetChild(fatherCount02).transform.position.y, 0), Quaternion.identity);
                obj02.GetComponent<SpriteRenderer>().sprite = obj02.GetComponent<EnemyController>().GiveSprite(x);
                obj02.transform.SetParent(transform.parent);
                obj02.GetComponent<EnemyController>().InitialSprites(initialObjectsSpeed);
                obj02.GetComponent<EnemyController>().SetChildUp(true);
            }
            else
            {
                var obj02 = Instantiate(goodObject, new Vector3(transform.GetChild(fatherCount02).transform.position.x, transform.GetChild(fatherCount02).transform.position.y, 0), Quaternion.identity);
                obj02.GetComponent<SpriteRenderer>().sprite = obj02.GetComponent<EnemyController>().GiveSprite(x);
                obj02.transform.SetParent(transform.parent);
                obj02.GetComponent<EnemyController>().InitialSprites(initialObjectsSpeed);
            }
        }
        spawnNumber.Clear();
    }

    private int RandomChoose()
    {
        int number = -1;
        number = (int)Random.Range(0, 6);
        if (spawnNumber.Contains(number))
        {
            return RandomChoose();
        }
        else
        {
            spawnNumber.Add(number);
        }
        return number;
    }
}
