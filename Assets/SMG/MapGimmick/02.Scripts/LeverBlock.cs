using UnityEngine;

public class LeverBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";

    public bool onlyBullet = true;

    public bool isOn = false;

    public AudioClip sfx;

    public Vector2 On_minXY = new Vector2(-1,-1);
    public Vector2 On_maxXY = new(0, 1);

    public Vector2 Off_minXY = new Vector2(0,-1);
    public Vector2 Off_maxXY = new(1, 1);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onlyBullet && !collision.gameObject.CompareTag(tagBullet))
            return;

        //Debug.Log("collision.transform.position: " + collision.transform.position);
        //Debug.Log("collision.GetContact(0).point: " + transform.InverseTransformPoint(collision.GetContact(0).point));

        Rect onArea = new Rect(On_minXY.x, On_minXY.y, 
            On_maxXY.x - On_minXY.x, 
            On_maxXY.y - On_minXY.y);

        Rect offArea = new Rect(Off_minXY.x, Off_minXY.y,
            Off_maxXY.x - Off_minXY.x,
            Off_maxXY.y - Off_minXY.y);

        //Debug.Log("onArea: " + onArea);
        //Debug.Log("rect: " + onArea.xMin + ", " + onArea.xMax + ", " + onArea.yMin + ", " + onArea.yMax);

        Debug.Log(onArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)));

        // On
        if(onArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)))
        {
            if(!isOn)
                SoundsPlayer.Instance.PlaySFX(sfx);
            isOn = true;
            
        }
        else if (offArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)))
        {
            if(isOn)
                SoundsPlayer.Instance.PlaySFX(sfx);
            isOn = false;
            
        }
    }

}
