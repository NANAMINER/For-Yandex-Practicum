using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject scorePrefab;
    public Vector3 spawnPosition;
    public float minHeight = 1f;
    public float maxHeight = 5f;
    public float minInterval = 1f;
    public float maxInterval = 5f;

    public void StartSpawn()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float height = Random.Range(minHeight, maxHeight);
            int a = Random.Range(0, 3);
            if(a == 0)
            {
                GameObject score = Instantiate(scorePrefab, transform.position + new Vector3(0, height, 0), Quaternion.identity);
            }
            else
            {
                GameObject obstacle = Instantiate(obstaclePrefab, transform.position + new Vector3(0, height, 0), Quaternion.identity);
            }
            

            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);
        }
    }
}
