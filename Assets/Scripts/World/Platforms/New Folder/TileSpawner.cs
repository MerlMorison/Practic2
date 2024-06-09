using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject floorPrefab; 
    [SerializeField] protected Transform player; 
    protected List<GameObject> activeTiles = new List<GameObject>(); 
    protected float spawnPos = 0;
    protected float tileLength = 20; 

    protected int startTiles = 6; 

    protected virtual void Start()
    {
        
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile();
        }
    }

    protected virtual void Update()
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

    protected virtual void SpawnTile()
    {
        
        GameObject nextTile = Instantiate(floorPrefab, transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);

        
        PlaceObjects(nextTile.transform);

        spawnPos += tileLength; 
    }

    protected virtual void PlaceObjects(Transform tile)
    {
     
    }

    protected virtual void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
