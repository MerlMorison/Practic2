using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : TileSpawner
{
    [SerializeField] private GameObject collectiblePrefab; 
    [SerializeField] private int collectibleCount = 5; 

    protected override void PlaceObjects(Transform tile)
    {
      
        List<Transform> collectibleSpawnPoints = new List<Transform>();
        foreach (Transform child in tile)
        {
            if (child.CompareTag("CollectibleSpawnPoint"))
            {
                collectibleSpawnPoints.Add(child);
            }
        }

       
        if (collectibleSpawnPoints.Count == 0)
        {
            Debug.LogWarning("Не найдено точек спауна для собираемых предметов на плитке.");
            return;
        }

       
        List<Transform> chosenSpawnPoints = new List<Transform>();
        for (int i = 0; i < Mathf.Min(collectibleCount, collectibleSpawnPoints.Count); i++)
        {
            int randomIndex = Random.Range(0, collectibleSpawnPoints.Count);
            chosenSpawnPoints.Add(collectibleSpawnPoints[randomIndex]);
            collectibleSpawnPoints.RemoveAt(randomIndex);
        }

        
        foreach (Transform spawnPoint in chosenSpawnPoints)
        {
            GameObject collectible = Instantiate(collectiblePrefab, spawnPoint.position, Quaternion.identity, tile);
            collectible.transform.Rotate(Vector3.up, 180f);
        }
    }
}
