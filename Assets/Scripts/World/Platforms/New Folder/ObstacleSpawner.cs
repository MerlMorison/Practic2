using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : TileSpawner
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] movingObstaclePrefabs;

    protected override void PlaceObjects(Transform tile)
    {

        List<Transform> obstacleSpawnPoints = new List<Transform>();
        List<Transform> movingObstacleSpawnPoints = new List<Transform>();
        foreach (Transform child in tile)
        {
            if (child.CompareTag("ObstacleSpawnPoint"))
            {
                obstacleSpawnPoints.Add(child);
            }
            else if (child.CompareTag("MovingObstacleSpawnPoint"))
            {
                movingObstacleSpawnPoints.Add(child);
            }
        }


        if (obstacleSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, obstacleSpawnPoints.Count);
            Transform spawnPoint = obstacleSpawnPoints[randomIndex];
            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacleIndex], spawnPoint.position, spawnPoint.rotation, tile);
        }

        if (movingObstacleSpawnPoints.Count > 0 && movingObstaclePrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, movingObstacleSpawnPoints.Count);
            Transform spawnPoint = movingObstacleSpawnPoints[randomIndex];
            int randomObstacleIndex = Random.Range(0, movingObstaclePrefabs.Length);
            Instantiate(movingObstaclePrefabs[randomObstacleIndex], spawnPoint.position, spawnPoint.rotation, tile);
        }
    }
}
