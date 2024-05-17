using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform player; // —сылка на игрока
    public GameObject[] tilePrefabs; // ћассив префабов плиток
    public int startTiles = 6; // Ќачальное количество плиток
    public float tileLength = 20f; // ƒлина каждой плитки
    public int maxTilesToKeep = 2; // ћаксимальное количество плиток, которое нужно хранить

    private List<GameObject> activeTiles = new List<GameObject>(); // —писок активных плиток
    private float spawnPosition = 0; // ѕозици€ дл€ спауна следующей плитки

    void Start()
    {
        // √енерируем начальный уровень
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        // ѕровер€ем позицию игрока и генерируем новые плитки по мере его движени€
        if (player.position.z - tileLength > spawnPosition - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        // —пауним плитку и обновл€ем позицию дл€ спауна следующей плитки
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPosition, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPosition += tileLength;
    }

    private void DeleteTile()
    {
        // ”дал€ем старую плитку, если количество активных плиток превышает максимальное значение
        if (activeTiles.Count > maxTilesToKeep)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
            spawnPosition -= tileLength;
        }
    }
}
