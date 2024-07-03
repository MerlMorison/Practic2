using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] movingObstaclePrefabs;
    [SerializeField] private Transform player;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 20;

    private int startTiles = 6;

    void Start()
    {

        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (player.position.z - 19 > spawnPos - (startTiles * tileLength))
            {
                SpawnTile();
                DeleteTile();
            }
        }
    }

    private void SpawnTile()
    {

        GameObject nextTile = Instantiate(floorPrefab, transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);


        PlaceObstacles(nextTile.transform);

        spawnPos += tileLength;
    }

    private void PlaceObstacles(Transform tile)
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

    private void DeleteTile()
    {

        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
