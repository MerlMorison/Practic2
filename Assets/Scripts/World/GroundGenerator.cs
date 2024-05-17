using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab; // Префаб пола
    [SerializeField] private GameObject[] obstaclePrefabs; // Массив префабов препятствий
    [SerializeField] private Transform player; // Ссылка на игрока
    private List<GameObject> activeTiles = new List<GameObject>(); // Список активных плит
    private float spawnPos = 0; // Позиция для спауна следующей плиты
    private float tileLength = 20; // Длина плиты

    private int startTiles = 6; // Количество начальных плит

    void Start()
    {
        // Создание начальных плит
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // Проверка, нужно ли создавать новую плиту
        if (player.position.z - 19 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(); // Создание новой плиты
            DeleteTile(); // Удаление плиты за игроком
        }
    }

    private void SpawnTile()
    {
        // Создание плиты (пола)
        GameObject nextTile = Instantiate(floorPrefab, transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);

        // Размещение препятствий на плите
        PlaceObstacles(nextTile.transform);

        spawnPos += tileLength; // Увеличение позиции спауна для следующей плиты
    }

    private void PlaceObstacles(Transform tile)
    {
        // Получаем все точки спауна препятствий на плите
        List<Transform> spawnPoints = new List<Transform>();
        foreach (Transform child in tile)
        {
            if (child.CompareTag("ObstacleSpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }

        // Если есть хотя бы одна точка спауна, выбираем случайную и размещаем на ней препятствие
        if (spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];
            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacleIndex], spawnPoint.position, spawnPoint.rotation, tile);
        }
    }

    private void DeleteTile()
    {
        // Удаление самой первой плиты из списка и уничтожение объекта
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
