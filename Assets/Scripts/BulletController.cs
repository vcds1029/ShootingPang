using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletController : MonoBehaviour
{
    [Header("Player Settings")]
    public float baseSpeed = 10f;             // 기본 속도 값 (스피드)
    public float powerMultiplier = 30f;       // 드래그에 따른 추가 속도 배율 (파워)
    public float powerExponent = 1.5f;        // 파워 민감도 (지수)
    public float maxLineLength = 1.5f;
    public float speedDampingBase = 0.98f;
    public float stopThreshold = 0.1f;

    [Header("Audio Clips")]
    public AudioClip backgroundHitSound;
    public AudioClip pointCollectSound;
    public AudioClip bombCollectSound;
    public AudioClip bombSfx;
    public AudioClip magneticSfx;
    public AudioClip knockBackSfx;

    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 releasePosition;
    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    private bool isStarted;
    private bool hasUsedItem;

    [SerializeField] public List<GameObject> itemParticles;
    public bool isDestroyed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 1.5f;
        rb.drag = 0f;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.white;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;

        isStarted = false;
        isDestroyed = false;
        hasUsedItem = false;
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.isGameReset)
        {
            Destroy(gameObject);
            PlayerController.Instance.isBulletSelected = false;
            PlayerController.Instance.selectAvailable = true;
            PlayerController.Instance.bulletDestroyed = true;
            GameManager.Instance.isGameReset = false;
        }

        if (isDestroyed)
        {
            DestroyBullet();
        }

        if (rb.linearVelocity.magnitude < stopThreshold && GameManager.Instance.isGameProceed)
        {
            if (Input.GetMouseButtonDown(0) && !isStarted && PlayerController.Instance.isPlayAvailable)
            {
                isDragging = true;
                lineRenderer.enabled = true;
                PlayerController.Instance.selectAvailable = false;
                PlayerController.Instance.bulletDestroyed = false;
            }

            if (isDragging && !isStarted && PlayerController.Instance.isPlayAvailable)
            {
                Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
                float dragMagnitude = Mathf.Min(dragVector.magnitude, maxLineLength);
                Vector2 limitedEndPosition = (Vector2)transform.position - dragVector.normalized * dragMagnitude;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, limitedEndPosition);

                // 드래그 세기에 따라 색상 변경
                float powerRatio = dragMagnitude / maxLineLength;
                lineRenderer.startColor = Color.Lerp(Color.green, Color.red, powerRatio);
                lineRenderer.endColor = Color.white;
            }

            if (Input.GetMouseButtonUp(0) && isDragging && !isDestroyed && !isStarted && PlayerController.Instance.isPlayAvailable)
            {
                isDragging = false;
                releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isStarted = true;

                Vector2 dragDistance = (Vector2)transform.position - releasePosition;
                float dragMagnitude = Mathf.Min(dragDistance.magnitude, maxLineLength);
                float ratio = dragMagnitude / maxLineLength;

                // 기본 스피드와 드래그 거리에 따른 추가 파워를 분리하여 계산
                float additionalSpeed = Mathf.Pow(ratio, powerExponent) * powerMultiplier;
                float finalSpeed = baseSpeed + additionalSpeed;

                rb.linearVelocity = dragDistance.normalized * finalSpeed;
                lineRenderer.enabled = false;
                GameManager.Instance.UseShot();

                float dynamicDamping = Mathf.Clamp(speedDampingBase + (baseSpeed - finalSpeed) * 0.005f, 0.95f, 0.99f);
                speedDampingBase = dynamicDamping;

                PlayerController.Instance.BulletUsed();
            }
        }

        if (isStarted && rb.linearVelocity == Vector2.zero && !isDestroyed && !hasUsedItem)
        {
            hasUsedItem = true;
            StartCoroutine(UseItemAndDestroy());
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity *= speedDampingBase;
        if (rb.linearVelocity.magnitude < stopThreshold)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator UseItemAndDestroy()
    {
        GetComponent<Magnetic>().Pull();
        SoundsPlayer.Instance.PlaySFX(magneticSfx);

        switch (PlayerController.Instance.selectedItem)
        {
            case 0:
                GetComponent<Bomb>().UseBomb();
                itemParticles[0].SetActive(true);
                SoundsPlayer.Instance.PlaySFX(bombSfx, 0.8f, 1f);
                break;
            case 1:
                GetComponent<KnockBack>().Push();
                itemParticles[1].SetActive(true);
                SoundsPlayer.Instance.PlaySFX(knockBackSfx);
                break;
            default:
                break;
        }

        PlayerController.Instance.UseItem(PlayerController.Instance.selectedItem);
        yield return new WaitForSeconds(1f);
        DestroyBullet();
    }

    void DestroyBullet()
    {
        PlayerController.Instance.MakeBullet();
        PlayerController.Instance.isBulletSelected = false;
        PlayerController.Instance.selectAvailable = true;
        PlayerController.Instance.bulletDestroyed = true;
        Destroy(gameObject);
        this.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("BackGround") || collision.gameObject.CompareTag("Block")) && backgroundHitSound != null)
        {
            audioSource.PlayOneShot(backgroundHitSound);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance == null) return;
    }
}
