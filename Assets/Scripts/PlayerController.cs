using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameObject BulletPrefab;
    public bool isBulletSelected = false;

    private ItemController itemController;
    public int selectedItem;
    public bool selectAvailable;



    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지됨
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 삭제
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemController = ItemController.Instance;
        selectAvailable = true;
        selectedItem = -1;

        Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        SelectItemEvent();
    }

    public void MakeIt()
    {
        Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.identity);
    }


    public void UseItem(int item)
    {
        if (itemController.UseItem(item))
        {
            isBulletSelected = false;
        }
        selectedItem = -1;
    }


    private void SelectItemEvent()
    {

        for (int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)) && selectAvailable && itemController.IsItemRemain(i))
            {
                Debug.Log($"SelectItemEvent called {i}");
                //if (selectedItem == i)
                //{
                //    selectedItem = -1;
                //    itemController.UnSelectItem();
                //}
                //else
                //{
                selectedItem = i;
                itemController.SelectItem(i);
                isBulletSelected = true;
                //}
            }
        }
    }
}