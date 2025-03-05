using Mono.Cecil;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    nextStage = 1
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<GameObject> stages;
    //private List<StageData> stageInfo = new List<StageData>();
    int currentStage;

    [SerializeField] private TextMeshProUGUI coinText;
    public int remainCoin;

    [SerializeField] private List<GameObject> Panels;

    public bool isGameProceed;

    private GameObject currentStageObject;


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
        //PrepareStage(currentStage);
        //stages[currentStage].SetActive(true);
        currentStageObject = Instantiate(stages[currentStage]); 
        isGameProceed = true;
    }

    private void Update()
    {
        //if (PlayerController.Instance.bulletPossess >= 0 && remainCoin == 0)
        //{
        //    // Win condition
        //    Panels[(int)Panel.nextStage].SetActive(true);
        //}
        //if (PlayerController.Instance.bulletPossess == 0 && PlayerController.Instance.bulletDestroyed && remainCoin != 0)
        //{
        //    // Lose condition
        //    Panels[(int)Panel.gameOver].SetActive(true);
        //    isGameProceed = false;
        //}
    }

    // DEPRECATED
    private void PrepareStage(int stage)
    {
        //foreach (GameObject s in stages)
        //{
        //    s.SetActive(false);
        //}
        //stages[stage].SetActive(true);

        //ItemController.Instance.items = stageInfo[stage].items;
        //PlayerController.Instance.bulletPossess = stageInfo[stage].bullet;
        //remainCoin = stageInfo[stage].coin;

        Instantiate(stages[stage]);
        isGameProceed = true;

        ItemController.Instance.ShowItem();
        PlayerController.Instance.UpdateBulletPossess();
        coinText.SetText($"Rest Coin \n X {remainCoin}");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        foreach (GameObject p in Panels)
        {
            p.SetActive(false);
        }
        //PrepareStage(++currentStage);
        isGameProceed = true;
        Destroy(currentStageObject);
        currentStageObject = Instantiate(stages[++currentStage]);
        //Panels[(int)Panel.nextStage].SetActive(true);
    }

    public void RetryStage()
    {
        //foreach (GameObject p in Panels)
        //{
        //    p.SetActive(false);
        //}
        //PrepareStage(currentStage);
        //isGameProceed = true;
        isGameProceed = true;
        Destroy(currentStageObject);
        currentStageObject = Instantiate(stages[currentStage]);
        Panels[(int)Panel.gameOver].SetActive(true);
        isGameProceed = false;
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
            RetryStage();
        }
    }


    public void RetryNow()
    {
        Panels[(int)Panel.gameOver].SetActive(false);
        Destroy(currentStageObject);
        currentStageObject = Instantiate(stages[currentStage]);
        isGameProceed = true;
    }


    public void InitCoin(int numCoin)
    {
        remainCoin = numCoin;
        coinText.SetText($"Rest Coin \n X {remainCoin}");
    }





    /***********************************
     * DEPRECATED UNDER THIS LINE
     ***********************************/

    //[Header("Game Settings")]
    //public int maxShots = 5;
    private int shotsLeft;
    private int score;
    //private Coroutine gameOverCheckCoroutine = null;

    //[Header("Object Settings")]
    //public GameObject pointPrefab;
    //public GameObject bombPrefab;
    //public GameObject wallPrefab;

    //public int pointCount = 3;
    //public int bombCount = 1;
    //public int wallCount = 2;

    //[Header("Spawn Area")]
    //public Vector2 spawnAreaMin = new Vector2(-7, -3.5f);
    //public Vector2 spawnAreaMax = new Vector2(7, 3.5f);

    //private bool isRespawning = false;
    //private Rigidbody2D playerRb;


    //private const float respawnDelay = 0.2f;
    //private const float gameOverCheckDelay = 1.0f;
    //private const float minVelocityThreshold = 0.1f;



    //void Start()
    //{
    //    shotsLeft = maxShots;
    //    score = 0;
    //    //UpdateUI(); DEPRECATED
    //    //SpawnObjects();

    //    playerRb = FindObjectOfType<Rigidbody2D>();
    //}

    //void Update()
    //{
    //    //if (!isRespawning && !IsPointObjectExist())
    //    //{
    //    //    CheckAndRespawnObjects();
    //    //}
    //}

    public void AddScore(int amount)
    {
        score += amount;
        //UpdateUI(); DEPRECATED
    }

    public void ModifyShots(int amount)
    {
        //shotsLeft += amount;
        ////UpdateUI(); DEPRECATED

        //if (shotsLeft == 0 && gameOverCheckCoroutine == null)
        //{
        //    gameOverCheckCoroutine = StartCoroutine(CheckGameOverCondition());
        //}
    }

    //private IEnumerator CheckGameOverCondition()
    //{
    //    yield return new WaitForSeconds(gameOverCheckDelay);

    //    while (playerRb != null && playerRb.linearVelocity.magnitude > minVelocityThreshold)
    //    {
    //        yield return null;
    //    }

    //    if (shotsLeft == 0)
    //    {
    //        // Update UI (DEPRECATED)
    //    }

    //    gameOverCheckCoroutine = null;
    //}

    //public bool CanShoot() => shotsLeft > 0;
    public bool CanShoot() => true;

    public void UseShot() => ModifyShots(-1);

    //public void CheckAndRespawnObjects()
    //{
    //    if (!isRespawning)
    //    {
    //        isRespawning = true;
    //        StartCoroutine(RespawnObjectsCoroutine());
    //    }
    //}

    //private IEnumerator RespawnObjectsCoroutine()
    //{
    //    yield return new WaitForSeconds(respawnDelay);

    //    foreach (GameObject bomb in GameObject.FindGameObjectsWithTag("Bomb"))
    //    {
    //        Destroy(bomb);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(respawnDelay);

    //    SpawnObjects();
    //    isRespawning = false;
    //}

    //private void SpawnObjects()
    //{
    //    SpawnMultipleObjects(pointPrefab, pointCount);
    //    SpawnMultipleObjects(bombPrefab, bombCount);
    //    SpawnMultipleObjects(wallPrefab, wallCount);
    //}

    //private void SpawnMultipleObjects(GameObject prefab, int count)
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        SpawnObject(prefab);
    //    }
    //}

    //private void SpawnObject(GameObject prefab)
    //{
    //    Vector2 randomPosition = new Vector2(
    //        Random.Range(spawnAreaMin.x, spawnAreaMax.x),
    //        Random.Range(spawnAreaMin.y, spawnAreaMax.y)
    //    );

    //    Instantiate(prefab, randomPosition, Quaternion.identity);
    //}

    //private bool IsPointObjectExist()
    //{
    //    return GameObject.FindGameObjectWithTag("Point") != null;
    //}
}
