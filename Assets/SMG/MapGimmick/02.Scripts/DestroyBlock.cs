using Unity.VisualScripting;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";
    //private string layerInvincible = "Invincible";

    public AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagBullet))
        {
            float playSFXTime = 1f;
            SoundsPlayer.Instance.PlaySFX(sfx, playSFXTime);

            // 레이어 예외처리
            //if (collision.gameObject.layer == LayerMask.NameToLayer(layerInvincible))
            //    Break;
            Destroy(collision.gameObject);
            MakeBullet();
        }
    }

    private void MakeBullet()
    {
        PlayerController.Instance.MakeBullet();
        PlayerController.Instance.isBulletSelected = false;
        PlayerController.Instance.selectAvailable = true;
        PlayerController.Instance.bulletDestroyed = true;
        PlayerController.Instance.UseItem(PlayerController.Instance.selectedItem);
    }

}
