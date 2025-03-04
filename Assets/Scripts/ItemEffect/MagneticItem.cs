using System.Collections;
using UnityEngine;

public class MagneticItem : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float pullPower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Magnetic();
        }
    }

    //타겟 당기기
    void Magnetic()
    {
        //스캔된 오브젝트들
        RaycastHit2D[] targets2D = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0, layerMask);

        foreach (RaycastHit2D target2D in targets2D)
        {
            if (target2D.transform.CompareTag("Coin"))
            {
                Vector2 dir = transform.position - target2D.transform.position;
                pullPower = dir.magnitude;
                dir = dir.normalized * pullPower;

                Rigidbody2D _rigid2D = target2D.transform.GetComponent<Rigidbody2D>();
                _rigid2D.AddForce(dir, ForceMode2D.Impulse);
            }
        }
    }
}
