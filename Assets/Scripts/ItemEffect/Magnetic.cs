using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [SerializeField] float radius; // 자석 범위
    [SerializeField] LayerMask layerMask; // 감지할 레이어
    [SerializeField] float powerMultiplier = 1.0f; // 자석의 계수

    // 타겟 당기기
    public void Pull()
    {
        // 스캔된 오브젝트들
        RaycastHit2D[] targets2D = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0, layerMask);

        foreach (RaycastHit2D target2D in targets2D)
        {
            if (target2D.transform.CompareTag("Coin"))
            {
                Vector2 dir = transform.position - target2D.transform.position;
                float pullPower = dir.magnitude * powerMultiplier;  // 힘 배율 적용
                dir = dir.normalized * pullPower;

                Rigidbody2D _rigid2D = target2D.transform.GetComponent<Rigidbody2D>();
                _rigid2D.AddForce(dir, ForceMode2D.Impulse);
            }
        }
    }
}
