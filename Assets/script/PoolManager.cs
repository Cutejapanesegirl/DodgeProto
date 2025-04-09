using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    // �����յ��� ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //  �� �̵� �Ŀ��� ����

            InitPool(); //  ���� Awake �ڵ� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //  Ǯ �ʱ�ȭ �ڵ�
    void InitPool()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ�� ����
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��� �� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� ã������?
        if (!select)
        {
            //���Ӱ� ���� �� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
