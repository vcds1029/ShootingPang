using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Bomb = 0,               // 폭탄: 일정 범위 내 아이템 획득
    KnockBack = 1,        // 밀치기: 일정 범위 내 아이템 밀치기
    Imotal = 2,             // 무적: 특정 기믹 회피
    Cleaner = 3,             // 세정제: 특정 기믹 회피
    ZeroGravity = 4      // 무중력: 특정 기믹 회피
}

public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; private set; }

    [SerializeField] public int[] items;
    private int selectedItem = -1;
    private Dictionary<Item, Color> itemColors = new Dictionary<Item, Color>
    {
        { Item.Bomb, Color.red },
        { Item.KnockBack, Color.blue },
        { Item.Imotal, Color.green },
        { Item.Cleaner, Color.yellow },
        { Item.ZeroGravity, Color.magenta }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ShowItem();
    }

    public bool UseItem(int item)
    {
        if (item == -1) return true;

        if (items[item] > 0)
        {
            items[item]--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectItem(int item)
    {
        if (selectedItem == item)
        {
            UnSelectItem();
            return;
        }

        ClearSelectItem();
        selectedItem = item;

        ApplyItemToBullet();
    }

    public void UnSelectItem()
    {
        ClearSelectItem();
        selectedItem = -1;
    }

    public void ClearSelectItem()
    {
        if (PlayerController.Instance.currentBullet != null)
        {
            SpriteRenderer bulletSpriteRenderer = PlayerController.Instance.currentBullet.GetComponent<SpriteRenderer>();
            if (bulletSpriteRenderer != null)
            {
                bulletSpriteRenderer.color = Color.black; // 기본 색상으로 변경
            }
        }
    }

    // 🔹 새로운 Bullet이 생성될 때 자동으로 적용되도록 함
    public void ApplyItemToBullet()
    {
        if (PlayerController.Instance.currentBullet == null) return;

        SpriteRenderer bulletSpriteRenderer = PlayerController.Instance.currentBullet.GetComponent<SpriteRenderer>();
        if (bulletSpriteRenderer != null)
        {
            if (selectedItem != -1 && itemColors.TryGetValue((Item)selectedItem, out Color newColor))
            {
                bulletSpriteRenderer.color = newColor; // 아이템 색상 적용
            }
            else
            {
                bulletSpriteRenderer.color = Color.black; // 기본값
            }
        }
    }

    public bool IsItemRemain(int item)
    {
        return items[item] > 0;
    }

    public void InitItem(int[] itemNums)
    {
        items[(int)Item.Bomb] = itemNums[(int)Item.Bomb];
        items[(int)Item.KnockBack] = itemNums[(int)Item.KnockBack];
        items[(int)Item.Imotal] = itemNums[(int)Item.Imotal];
        // ShowItem();
    }
}
