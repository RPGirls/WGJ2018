using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float minTimeToSpawn;
    public float maxTimeToSpawn;
    public GameObject badObject;
    public GameObject goodObject;

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
        float time = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        yield return new WaitForSeconds(time);
        SpawnEnemy();
        StartCoroutine("EnemySpawner");
    }

    private void SpawnEnemy()
    {
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(badObject, new Vector3(transform.GetChild((int)(Random.Range(0, (float)transform.childCount))).transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(goodObject, new Vector3(transform.GetChild((int)(Random.Range(0, (float)transform.childCount))).transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        }
    }

    public void SpawnInitial()
    {

    }
}
