using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public struct StageData
{
    public int[] items;
    public int bullet;
    public int coin;

    public StageData(int[] items, int bullet, int coin)
    {
        this.items = items;
        this.bullet = bullet;
        this.coin = coin;
    }
}

public enum Panel
{
    gameOver = 0,
    nextStage = 1,
    gameEnd = 2
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<GameObject> stages;
    //private List<StageData> stageInfo = new List<StageData>();
    public int currentStage;

    [SerializeField] private TextMeshProUGUI coinText;
    public int remainCoin;

    [SerializeField] private List<GameObject> Panels;

    public bool isGameProceed;
    public bool isGameReset;
   

    private GameObject currentStageObject;

    public bool canRetry;
    private bool isStageEnd;
    public int bulletUsed;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentStage = 0;
        currentStageObject = Instantiate(stages[currentStage]);
        isGameProceed = true;
        canRetry = true;
        isStageEnd = false;

    }

    private void Update()
    {

    }


    public void NextStage()
    {
        StopAllCoroutines();

        if (currentStage == 3)
        {
            foreach (GameObject panel in Panels)
            {
                panel.SetActive(false);
            }
            Panels[(int)Panel.gameEnd].SetActive(true);
        }
        else
        {
            Panels[(int)Panel.nextStage].SetActive(false);
            isGameProceed = true;
            isGameReset = true;
            Destroy(currentStageObject);
            currentStageObject = Instantiate(stages[++currentStage]);
            //Panels[(int)Panel.nextStage].SetActive(true);
            PlayerController.Instance.MakeBullet();
            ItemController.Instance.UnSelectItem();
            PlayerController.Instance.selectedItem = -1;
        }
    }

    public void RetryStage()
    {
        isGameProceed = true;
        Destroy(currentStageObject);
        currentStageObject = Instantiate(stages[currentStage]);
        Panels[(int)Panel.gameOver].SetActive(true);
        isGameProceed = false;
        ItemController.Instance.UnSelectItem();
        PlayerController.Instance.selectedItem = -1;
    }

    public void GainCoin()
    {
        remainCoin--;
        coinText.SetText($"Rest Coin \n X {remainCoin}");

        if (remainCoin == 0)
        {
            isGameProceed = false;
            //NextStage();
            Panels[(int)Panel.nextStage].SetActive(true);
        }
    }

    public void NoBullet()
    {
        if (remainCoin != 0)
        {
            isStageEnd = true;
            StartCoroutine(LateRetry(4.0f));
        }
    }

    private IEnumerator LateRetry(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (remainCoin == 0)
        {
            isGameProceed = false;
            //NextStage();
            Panels[(int)Panel.nextStage].SetActive(true);
        }
        else
        {
            isGameProceed = false;
            RetryStage();
        }
    }


    public void RetryNow()
    {
        if (canRetry)
        {
            StopAllCoroutines();

            Panels[(int)Panel.gameOver].SetActive(false);
            Destroy(currentStageObject);
            currentStageObject = Instantiate(stages[currentStage]);
            isGameProceed = true;
            isGameReset = true;
            //StartCoroutine(RestartCooltime());

            //Destroy(GameObject.Find("Bullet(Clone)"));
            PlayerController.Instance.MakeBullet();
            ItemController.Instance.UnSelectItem();
            PlayerController.Instance.selectedItem = -1;
            PlayerController.Instance.isPlayAvailable = true;

            isStageEnd = false;
        }
    }

    private IEnumerator RestartCooltime()
    {
        yield return new WaitForSeconds(0.1f);
        isGameReset = false;
    }


    public void InitCoin(int numCoin)
    {
        remainCoin = numCoin;
        coinText.SetText($"Rest Coin \n X {remainCoin}");
    }


    private int shotsLeft;
    private int score;

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void ModifyShots(int amount)
    {

    }

    public bool CanShoot() => true;

    public void UseShot() => ModifyShots(-1);

}
