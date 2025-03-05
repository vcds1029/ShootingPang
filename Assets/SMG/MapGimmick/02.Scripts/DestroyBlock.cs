using Unity.VisualScripting;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";
    //private string layerInvincible = "Invincible";

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
            // 레이어 예외처리
            //if (collision.gameObject.layer == LayerMask.NameToLayer(layerInvincible))
            //    Break;
            Destroy(collision.gameObject);
            Invoke("MakeIt", 2f);
        }
    }

    private void MakeIt()
    {
        PlayerController.Instance.MakeIt();
    }

}
