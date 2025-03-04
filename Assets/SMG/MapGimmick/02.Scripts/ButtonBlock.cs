using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    public bool isPush;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPush = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPush = true;
    }
}
