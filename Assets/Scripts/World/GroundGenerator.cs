using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab; // ������ ����
    [SerializeField] private GameObject[] obstaclePrefabs; // ������ �������� �����������
    [SerializeField] private Transform player; // ������ �� ������
    private List<GameObject> activeTiles = new List<GameObject>(); // ������ �������� ����
    private float spawnPos = 0; // ������� ��� ������ ��������� �����
    private float tileLength = 20; // ����� �����

    private int startTiles = 6; // ���������� ��������� ����

    void Start()
    {
        // �������� ��������� ����
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // ��������, ����� �� ��������� ����� �����
        if (player.position.z - 19 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(); // �������� ����� �����
            DeleteTile(); // �������� ����� �� �������
        }
    }

    private void SpawnTile()
    {
        // �������� ����� (����)
        GameObject nextTile = Instantiate(floorPrefab, transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);

        // ���������� ����������� �� �����
        PlaceObstacles(nextTile.transform);

        spawnPos += tileLength; // ���������� ������� ������ ��� ��������� �����
    }

    private void PlaceObstacles(Transform tile)
    {
        // �������� ��� ����� ������ ����������� �� �����
        List<Transform> spawnPoints = new List<Transform>();
        foreach (Transform child in tile)
        {
            if (child.CompareTag("ObstacleSpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }

        // ���� ���� ���� �� ���� ����� ������, �������� ��������� � ��������� �� ��� �����������
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
        // �������� ����� ������ ����� �� ������ � ����������� �������
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
