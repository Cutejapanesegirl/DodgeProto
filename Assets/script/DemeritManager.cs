using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemeritManager : MonoBehaviour
{
    public BulletSpawner[] bulletSpawners; //  ��� BulletSpawner ����
    Item[] items;

    void Awake()
    {
        bulletSpawners = FindObjectsOfType<BulletSpawner>(); // ��� BulletSpawner ã��

        foreach (var spawner in bulletSpawners)
        {
            spawner.currentBulletIndex = 0; // �Ѿ� ������ �ε��� �ʱ�ȭ
            spawner.spawnRateMin = 0.5f;    //  �⺻ ���� �ӵ� ����
            spawner.spawnRateMax = 3f;
        }

    }

    public void UpgradeBullet()
    {
        foreach (var spawner in bulletSpawners) //  ��� BulletSpawner�� ����
        {
            if (spawner != null && spawner.bulletPrefabs.Length > 0)
            {
                int nextBulletIndex = (spawner.currentBulletIndex + 1) % spawner.bulletPrefabs.Length;
                spawner.currentBulletIndex = nextBulletIndex;
            }
        }
    }


    public void UpgradeSpawner(float rate)
    {
        foreach (var spawner in bulletSpawners) //  ��� BulletSpawner�� ����
        {
            if (spawner != null)
            {
                spawner.spawnRateMin = Mathf.Max(0.05f, spawner.spawnRateMin - rate);
                spawner.spawnRateMax = Mathf.Max(0.2f, spawner.spawnRateMax - rate);
            }
        }
    }

    public void Next()
    {
        items = GetComponentsInChildren<Item>(true);

        Debug.Log("Next() ȣ���! ���� ������ ����: " + items.Length);
        foreach (var item in items)
        {
            Debug.Log("������ �̸�: " + item.name);
        }

        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

            if (ranItem.level == ranItem.data.damages.Length)
            {
                // items[Random.Range(4, 7)].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}