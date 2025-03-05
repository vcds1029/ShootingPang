using UnityEngine;

public class BeInvincible : MonoBehaviour
{
    LayerMask mask = 1 << 7;

    public void GetInvincible()
    {
        gameObject.layer = mask;
    }
}
