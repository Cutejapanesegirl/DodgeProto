using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    public float timer;
    public float cooltime = 10f;
    public bool hasShield = false;
    private int cooltimeReductionCount = 0;

    public GameObject ShieldEffect;  // ���� ����Ʈ ������ (Inspector���� ����)
    public GameObject activeShield;
    public GameObject BulletPrefab;
    public GameObject bulletSpawnerPrefab;

    public PlayerController player;

    public Bullet bulletScript;
    public BulletSpawner bulletSpawner;

    public float originalRate; // ���� ����� ��ġ ����� ����
    private bool isEffectRemoved = false;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        if (type == ItemData.ItemType.Item)  // ���� �������� ���� Ÿ�̸� ����
        {
            timer += Time.deltaTime;

            if (timer >= cooltime)
            {
                timer = 0f;
                Shield();
                Debug.Log("���尡 �� ������!");  // ������� ����
            }
        }
    }


    public void Init(ItemData data)
    {
        // Basic
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property
        type = data.itemType;
        rate = data.damages[0];

        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();

    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
            case ItemData.ItemType.Size:
                SizeDown();
                break;
            case ItemData.ItemType.Item:
                Shield();
                Debug.Log("ó�� ���尡 ������!");

                //  ��Ÿ���� �ִ� 5�������� ���ҽ�Ű��
                if (cooltimeReductionCount < 5)
                {
                    cooltime = cooltime - rate;
                    cooltimeReductionCount++;  // ���� Ƚ�� ����
                    Debug.Log("��Ÿ�� ���� ����! ���� Ƚ��: " + cooltimeReductionCount);
                }

                timer = 0f;
                break;
        }
    }

    public void RemoveGearEffects()
    {
        if (isEffectRemoved) return; // �̹� ȿ���� ���ŵ� ���¶�� �ߺ� ���� ����

        originalRate = rate; // ���� Gear ȿ�� ���� ����
        isEffectRemoved = true;

        switch (type)
        {
            case ItemData.ItemType.Shoe:
                GameManager.instance.player.speed = GameManager.instance.player.baseSpeed;
                break;
            case ItemData.ItemType.Size:
                GameManager.instance.player.transform.localScale = GameManager.instance.player.baseScale;
                break;
            case ItemData.ItemType.Item:
                if (activeShield != null)
                    Destroy(activeShield);
                break;
        }

        Debug.Log("Gear ȿ�� ���� �Ϸ�! ���� rate: " + originalRate);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Boss8") // Ư�� ���� �� ȿ�� ����
        {
            RemoveGearEffects();
            return;
        }

        if (isEffectRemoved) // ���� ������ ȿ���� ������ ���
        {
            isEffectRemoved = false; // �ٽ� Ȱ��ȭ�� ���̹Ƿ� �ʱ�ȭ
            rate = originalRate; // ����� ���� �� ����
            ApplyGear(); // Gear ȿ�� �ٽ� ����
            Debug.Log("Gear ȿ�� ������! rate: " + rate);
        }
    }

    void SpeedUp()
    {
        float speed = 8;
        GameManager.instance.player.speed = speed + speed * rate;
    }

    void SizeDown()
    {
        GameManager.instance.player.transform.localScale -= GameManager.instance.player.transform.localScale * rate;
    }

    void Shield()
    {
        player = GameManager.instance.player;

        if (player != null)
        {
            player.gear.hasShield = true;

            if (activeShield != null)
            {
                Destroy(activeShield);
            }

            // ���� ������ ����
            activeShield = Instantiate(ShieldEffect, player.transform.position, Quaternion.identity, player.transform);
        }
    }
}





