using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int maxShots = 5;
    private int shotsLeft;
    private int score;
    private Coroutine gameOverCheckCoroutine = null;

    [Header("Object Settings")]
    public GameObject pointPrefab;
    public GameObject bombPrefab;
    public GameObject wallPrefab;

    public int pointCount = 3;
    public int bombCount = 1;
    public int wallCount = 2;

    [Header("Spawn Area")]
    public Vector2 spawnAreaMin = new Vector2(-7, -3.5f);
    public Vector2 spawnAreaMax = new Vector2(7, 3.5f);

    private bool isRespawning = false;
    private Rigidbody2D playerRb;


    private const float respawnDelay = 0.2f;
    private const float gameOverCheckDelay = 1.0f;
    private const float minVelocityThreshold = 0.1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        shotsLeft = maxShots;
        score = 0;
        //UpdateUI(); DEPRECATED
        //SpawnObjects();

        playerRb = FindObjectOfType<Rigidbody2D>();
    }

    void Update()
    {
        //if (!isRespawning && !IsPointObjectExist())
        //{
        //    CheckAndRespawnObjects();
        //}
    }

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

    private IEnumerator CheckGameOverCondition()
    {
        yield return new WaitForSeconds(gameOverCheckDelay);

        while (playerRb != null && playerRb.linearVelocity.magnitude > minVelocityThreshold)
        {
            yield return null;
        }

        if (shotsLeft == 0)
        {
            // Update UI (DEPRECATED)
        }

        gameOverCheckCoroutine = null;
    }

    public bool CanShoot() => shotsLeft > 0;

    public void UseShot() => ModifyShots(-1);

    public void CheckAndRespawnObjects()
    {
        if (!isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnObjectsCoroutine());
        }
    }

    private IEnumerator RespawnObjectsCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        foreach (GameObject bomb in GameObject.FindGameObjectsWithTag("Bomb"))
        {
            Destroy(bomb);
            yield return null;
        }

        yield return new WaitForSeconds(respawnDelay);

        SpawnObjects();
        isRespawning = false;
    }

    private void SpawnObjects()
    {
        SpawnMultipleObjects(pointPrefab, pointCount);
        SpawnMultipleObjects(bombPrefab, bombCount);
        SpawnMultipleObjects(wallPrefab, wallCount);
    }

    private void SpawnMultipleObjects(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnObject(prefab);
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(prefab, randomPosition, Quaternion.identity);
    }

    private bool IsPointObjectExist()
    {
        return GameObject.FindGameObjectWithTag("Point") != null;
    }
}
