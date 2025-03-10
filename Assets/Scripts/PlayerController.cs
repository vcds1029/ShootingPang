using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameObject BulletPrefab;
    public bool isBulletSelected = false;

    private ItemController itemController;
    public int selectedItem;
    public bool selectAvailable;

    [SerializeField] private TextMeshProUGUI bulletText;
    public int bulletPossess;
    public bool bulletDestroyed;

    public bool isPlayAvailable;
    public GameObject currentBullet;


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지됨
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

        MakeBullet();

        bulletDestroyed = false;
        isPlayAvailable = true;
    }

    private void Update()
    {
        SelectItemEvent();
        SelectRetryEvent();
    }

   public void MakeBullet()
{
    currentBullet = Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.identity);
    // 블렛 재생성 시 아이템 선택 상태 초기화
    isBulletSelected = false;
    selectedItem = -1;
    itemController.UnSelectItem();
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
                // 이전 상태와 관계없이 바로 선택하도록 함
                selectedItem = i;
                itemController.SelectItem(i);
                isBulletSelected = true;
            }
        }
    }

    private void SelectRetryEvent()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RetryNow();
        }
    }

    public void UpdateBulletPossess()
    {
        bulletText.text = "Rest Bullet \n";
        bulletText.text += $"X {bulletPossess}";
    }

    public void BulletUsed()
    {
        bulletPossess--;
        UpdateBulletPossess();

        if (bulletPossess == 0)
        {
            //bulletAvailable = false;
            //StartCoroutine(LateCallNoBullet());

            isPlayAvailable = false;
            GameManager.Instance.NoBullet();
        }
    }


    public void InitBullet(int numBullet)
    {
        bulletPossess = numBullet;
        UpdateBulletPossess();
    }

    public void InitPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}