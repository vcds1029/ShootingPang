using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Player Settings")]
    public float maxPower = 40f;
    public float maxLineLength = 1.5f;
    public float speedDamping = 0.98f;
    public float stopThreshold = 0.1f;

    [Header("Audio Clips")]
    public AudioClip backgroundHitSound;
    public AudioClip pointCollectSound;
    public AudioClip bombCollectSound;

    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 releasePosition;
    private LineRenderer lineRenderer;
    private AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 1.5f;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.white;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (rb.linearVelocity.magnitude < stopThreshold && GameManager.Instance.CanShoot())
        {
            if (Input.GetMouseButtonDown(0) && PlayerController.Instance.isBulletSelected)
            {
                isDragging = true;
                lineRenderer.enabled = true;
            }

            if (isDragging)
            {
                Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (Vector2)transform.position - currentMousePosition;

                float dragMagnitude = Mathf.Min(dragVector.magnitude, maxLineLength);
                Vector2 limitedEndPosition = (Vector2)transform.position - dragVector.normalized * dragMagnitude;

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, limitedEndPosition);
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                isDragging = false;
                releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 dragDistance = (Vector2)transform.position - releasePosition;
                float dragMagnitude = Mathf.Min(dragDistance.magnitude, maxLineLength);
                float launchPower = (dragMagnitude / maxLineLength) * maxPower;

                rb.linearVelocity = dragDistance.normalized * launchPower;
                lineRenderer.enabled = false;
                GameManager.Instance.UseShot();

                Invoke("DestroyBullet", 2f);
            }
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
        PlayerController.Instance.MakeIt();
        PlayerController.Instance.isBulletSelected = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity *= speedDamping;
        if (rb.linearVelocity.magnitude < stopThreshold)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("BackGround") || collision.gameObject.CompareTag("Wall")) && backgroundHitSound != null)
        {
            audioSource.PlayOneShot(backgroundHitSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance == null) return;

        //vcds1029_PointObject pointObject = collision.GetComponent<vcds1029_PointObject>();

        //if (pointObject != null)
        //{
        //    if (collision.CompareTag("Point") && pointCollectSound != null)
        //        audioSource.PlayOneShot(pointCollectSound);
        //    else if (collision.CompareTag("Bomb") && bombCollectSound != null)
        //        audioSource.PlayOneShot(bombCollectSound);

        //    GameManager.Instance.AddScore(pointObject.pointValue);
        //    GameManager.Instance.ModifyShots(pointObject.shotBonus);
        //    Destroy(collision.gameObject);
        //}
    }
}
