using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab; // ������ ������ ������
    public float initialDelay = 1f;  // ���� ���� ��� �ð�
    public float spawnInterval = 1f; // ������ �߰� ����
    public float spawnDelay = 1f;    // �� ������ �� ���� ����
    public float spawnHeight = 1f;   // ������ ���� ����
    public Transform playerTransform;
    public Transform rotatingPlatform;

    private int spawnerCount = 1; // ������ ������ ����
    private const float squareSize = 15f; // ������ ���� ũ��

    private void Start()
    {
        StartCoroutine(StartSpawningAfterDelay());

        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>()?.transform;
            if (playerTransform == null)
                Debug.LogError("PlayerController�� ã�� �� �����ϴ�.");
        }

        if (rotatingPlatform == null)
            Debug.LogError("ȸ���ϴ� �÷����� �������� �ʾҽ��ϴ�.");
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(SpawnSpawnersAroundSquare());
    }

    private IEnumerator SpawnSpawnersAroundSquare()
    {
        List<Vector3> spawnPositions = GenerateSpawnPositions();
        Shuffle(spawnPositions);

        foreach (Vector3 position in spawnPositions)
        {
            SpawnSpawner(position);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private List<Vector3> GenerateSpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        float halfSize = squareSize / 2f;
        float midPoint = halfSize / 2f;

        // �簢�� ��踦 ���� ��ġ �߰�
        for (float i = -halfSize; i <= halfSize; i += 2f)
        {
            if (Mathf.Abs(i) > midPoint)
            {
                positions.Add(new Vector3(i, spawnHeight, halfSize));  // ���
                positions.Add(new Vector3(i, spawnHeight, -halfSize)); // �ϴ�
                positions.Add(new Vector3(halfSize, spawnHeight, i));  // ����
                positions.Add(new Vector3(-halfSize, spawnHeight, i)); // ����
            }
        }
        return positions;
    }

    private void SpawnSpawner(Vector3 position)
    {

        GameObject newSpawner = Instantiate(spawnerPrefab, position, Quaternion.identity, rotatingPlatform); 
        newSpawner.name = $"Bullet Spawner {spawnerCount++}";

        BulletSpawner bulletSpawner = newSpawner.GetComponent<BulletSpawner>();
        if (bulletSpawner != null)
            bulletSpawner.target = playerTransform;
        else
            Debug.LogError("BulletSpawner ��ũ��Ʈ�� ã�� �� �����ϴ�.");
    }

    private void Shuffle(List<Vector3> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}
