using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Bullet"))
        {
            GameManager.Instance.GainCoin();
            Destroy(transform.parent.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Bullet"))
    //    {
    //        GameManager.Instance.AddScore(1);
    //        transform.parent.gameObject.SetActive(false);
    //    }
    //}
}
