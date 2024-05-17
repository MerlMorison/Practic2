using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public GameObject[] tilePrefabs; // ������ �������� ������
    public int startTiles = 6; // ��������� ���������� ������
    public float tileLength = 20f; // ����� ������ ������
    public int maxTilesToKeep = 2; // ������������ ���������� ������, ������� ����� �������

    private List<GameObject> activeTiles = new List<GameObject>(); // ������ �������� ������
    private float spawnPosition = 0; // ������� ��� ������ ��������� ������

    void Start()
    {
        // ���������� ��������� �������
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        // ��������� ������� ������ � ���������� ����� ������ �� ���� ��� ��������
        if (player.position.z - tileLength > spawnPosition - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        // ������� ������ � ��������� ������� ��� ������ ��������� ������
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPosition, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPosition += tileLength;
    }

    private void DeleteTile()
    {
        // ������� ������ ������, ���� ���������� �������� ������ ��������� ������������ ��������
        if (activeTiles.Count > maxTilesToKeep)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
            spawnPosition -= tileLength;
        }
    }
}
