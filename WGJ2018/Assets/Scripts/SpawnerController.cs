using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float minTimeToSpawn;
    public float maxTimeToSpawn;
    public GameObject badObject;
    public GameObject goodObject;

    private void Start()
    {
        SpawnEnemy();
        StartCoroutine("EnemySpawner");
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
            Instantiate(badObject, new Vector3(Random.Range(-8, 8), this.transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(goodObject, new Vector3(Random.Range(-8, 8), this.transform.position.y, 0), Quaternion.identity);

        }
    }
}
