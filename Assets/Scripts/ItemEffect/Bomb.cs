using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] CircleCollider2D _circleColl;

    public void UseBomb()
    {
        _circleColl.enabled = true;
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Wall"))
    //    {
    //        Debug.Log("Explosion!");
    //    }
    //}
}
