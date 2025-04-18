using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    TMP_Text textLevel;
    TMP_Text textName;
    TMP_Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    void OnEnable()
    {
        TryReconnectGear();

        Debug.Log($"{name} OnEnable 호출됨. 현재 Level: {level}");
        textLevel.text = $"Lv. {level + 1}";

        if (data.itemType == ItemData.ItemType.Size ||
            data.itemType == ItemData.ItemType.Shoe ||
            data.itemType == ItemData.ItemType.Item ||
            data.itemType == ItemData.ItemType.Speed ||
            data.itemType == ItemData.ItemType.Spawn)
        {
            float percentage = data.damages[Mathf.Min(level, data.damages.Length - 1)] * 100f;
            textDesc.text = string.Format(data.itemDesc, percentage);
        }
    }

    void Start()
    {
        TryReconnectGear();
    }

    void TryReconnectGear()
    {
        Gear[] gears = FindObjectsOfType<Gear>();
        foreach (var g in gears)
        {
            if (g.itemId == data.itemId)
            {
                gear = g;
                level = g.level;
                break;
            }
        }
    }

    public void OnClick()
    {
        Debug.Log($"[Before] {name} Level: {level}");

        switch (data.itemType)
        {
            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.Size:
            case ItemData.ItemType.Item:
                if (gear == null)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();

                    if (data.itemType == ItemData.ItemType.Item)
                    {
                        gear.ShieldEffect = Resources.Load<GameObject>("Prefabs/ShieldEffect");
                    }

                    gear.Init(data); // Gear에 itemId, level = 1 설정됨
                }
                else
                {
                    float nextRate = data.damages[Mathf.Min(level, data.damages.Length - 1)];
                    gear.originalRate = nextRate;
                    gear.LevelUp(nextRate);
                }
                break;

            case ItemData.ItemType.Speed:
            case ItemData.ItemType.Spawn:
                // 향후 Weapon 구현 시 사용 예정
                break;
        }

        level++;
        Debug.Log($"[After] {name} Level: {level}");

        if (level >= data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
