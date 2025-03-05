using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum Item
{
    Bomb = 0,               // 폭탄: 일정 범위 내 아이템 획득
    Magnet = 1,             // 자석: 일정 범위 내 아이템 당기기
    KnockBack = 2,        // 밀치기: 일정 범위 내 아이템 밀치기
    Imotal = 3,             // 무적: 특정 기믹 회피
    Cleaner = 4,             // 세정제: 특정 기믹 회피
    ZeroGravity = 5      // 무중력: 특정 기믹 회피
}


public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; private set; }

    [SerializeField] private List<Image> itemImage;
    [SerializeField] public int[] items;

    private string[] itemStr = { "Bomb", "Magnet", "KnockBack", "Imotal", "Cleaner", "ZeroGravity"};


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
        //InitItem();
        ShowItem();
    }


    private void InitItem()
    {
        items[(int)Item.Bomb] = 2;
        items[(int)Item.Magnet] = 2; 
        items[(int)Item.KnockBack] = 2; 
    }

    public void ShowItem()
    {
        // Function on Image
        for (int i = 0; i < itemImage.Count; i++)
        {
            TextMeshProUGUI imageText = itemImage[i].GetComponentInChildren<TextMeshProUGUI>();
            if (imageText != null)
            {
                if (items[i] == 0)
                {
                    imageText.text = "X";
                }
                else
                {
                    imageText.text = itemStr[i] + "\n" + items[i].ToString();
                }
            }
        }
    }

    public bool UseItem(int item)
    {
        if (item == -1) return true;

        if (items[item] > 0)
        {
            items[item]--;
            ShowItem();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectItem(int item)
    {
        ClearSelectItem();
        itemImage[item].color = Color.cyan;
    }

    public void UnSelectItem()
    {
        ClearSelectItem();
    }

    public void ClearSelectItem()
    {
        foreach (Image img in itemImage)
        {
            img.color = Color.white;
        }
    }

    public bool IsItemRemain(int item)
    {
        if (items[item] == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

