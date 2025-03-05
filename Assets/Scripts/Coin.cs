using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            //GameManager.Instance.AddScore(1);
            GameManager.Instance.GainCoin();
            Destroy(gameObject);
        }
    }
}
