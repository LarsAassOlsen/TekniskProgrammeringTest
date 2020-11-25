using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObstacleSpawner : MonoBehaviour
{
    float spawnCountdown = 2;

    private void Start()
    {
        spawnCountdown = (float)Random.Range(1,3);
    }

    private void Update()
    {
        if (GameController.Instance.GameRunning)
        {
            spawnCountdown -= Time.deltaTime * GameController.Instance.GameSpeed;

            if (spawnCountdown <= 0)
            {
                SpawnPickupObstacle();
            }
        }
    }

    void SpawnPickupObstacle()
    {
        int _whatToSpawn = Random.Range(0, 10);
        if (_whatToSpawn <= 7)
        {
            Instantiate(GameController.Instance.PickupPrefab, transform.position, Quaternion.identity, GameController.Instance.PickupObstacleParent);
        }
        else
        {
            Instantiate(GameController.Instance.ObstaclePrefab, transform.position, Quaternion.identity, GameController.Instance.PickupObstacleParent);
        }
        spawnCountdown = Random.Range(GameController.Instance.SpawnTimeRangeMin, GameController.Instance.SpawnTimeRangeMax);
    }
}
