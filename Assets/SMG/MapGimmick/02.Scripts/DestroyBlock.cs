using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagBullet))
        {
            Destroy(collision.gameObject);
            Invoke("MakeIt", 2f);
        }
    }

    private void MakeIt()
    {
        PlayerController.Instance.MakeIt();
    }

}
