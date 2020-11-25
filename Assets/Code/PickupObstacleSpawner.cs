using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObstacleSpawner : MonoBehaviour
{
    float spawnCountdown = 2;
    public float ObstacleCooldown;
    public int SpawnerIndex;

    private void Start()
    {
        spawnCountdown = (float)Random.Range(1,3);
    }

    private void Update()
    {
        if (GameController.Instance.GameRunning)
        {
            spawnCountdown -= Time.deltaTime * GameController.Instance.GameSpeed;
            if(ObstacleCooldown > 0)
            {
                ObstacleCooldown -= Time.deltaTime;
            }
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
            SpawnPickup();
        }
        else
        {
            // Checks if neighbors have spawned obstacled recently
            if(GameController.Instance.Lanes[Mathf.Clamp(SpawnerIndex - 1, 0, GameController.Instance.Lanes.Count - 1)].PickupObstacleSpawner.ObstacleCooldown > 0 ||
               GameController.Instance.Lanes[Mathf.Clamp(SpawnerIndex + 1, 0, GameController.Instance.Lanes.Count - 1)].PickupObstacleSpawner.ObstacleCooldown > 0 ||
               ObstacleCooldown > 0)
            {
                SpawnPickup();
            }
            else
            {
                SpawnObstacle();
            }
        }
        spawnCountdown = Random.Range(GameController.Instance.SpawnTimeRangeMin, GameController.Instance.SpawnTimeRangeMax);
    }

    void SpawnPickup()
    {
        Instantiate(GameController.Instance.PickupPrefab, transform.position, Quaternion.identity, GameController.Instance.PickupObstacleParent);
    }

    void SpawnObstacle()
    {
        Instantiate(GameController.Instance.ObstaclePrefab, transform.position, Quaternion.identity, GameController.Instance.PickupObstacleParent);
        ObstacleCooldown = GameController.Instance.ObstacleSpawnCooldown;
    }
}
