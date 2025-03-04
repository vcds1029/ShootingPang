using UnityEngine;

public class PortalBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";


    public GameObject OutPortal;

    public bool isOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagBullet))
        {
            if(isOut == false)
            {
                OutPortal.GetComponent<PortalBlock>().isOut = true;
                collision.gameObject.transform.position = OutPortal.transform.position;
            }
        }
    }


}
