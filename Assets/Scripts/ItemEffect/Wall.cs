using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody2D _rigid2D;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigid2D.linearVelocity != Vector2.zero)
        {
            _rigid2D.linearVelocity -= (_rigid2D.linearVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            gameObject.SetActive(false);
            collision.transform.root.gameObject.SetActive(false);
        }
    }
}
