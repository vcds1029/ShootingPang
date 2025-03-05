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

        //bulletPossess = 5; // Temp bullet 
        //UpdateBulletPossess();

        bulletDestroyed = false;
    }

    private void Update()
    {
        SelectItemEvent();
        SelectRetryEvent();
    }

    public void MakeBullet()
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
                if (selectedItem == i)
                {
                    selectedItem = -1;
                    itemController.UnSelectItem();
                }
                else
                {
                    selectedItem = i;
                    itemController.SelectItem(i);
                    isBulletSelected = true;
                }
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
            StartCoroutine(LateCallNoBullet());
        }
    }

    private IEnumerator LateCallNoBullet()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.Instance.NoBullet();
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