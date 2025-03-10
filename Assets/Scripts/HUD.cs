using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum infoType { Stage, Bullet, BombCnt, KnockBack , Retry}
    public infoType type;

    Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case infoType.Stage:
                myText.text = string.Format("Stage:{0:F0}", GameManager.Instance.currentStage + 1);
                break;
            case infoType.Bullet:
                myText.text = string.Format("Bullet:{0:F0}", PlayerController.Instance.bulletPossess);
                break;
            case infoType.BombCnt:
                myText.text = string.Format("{0:F0}", ItemController.Instance.items[(int)Item.Bomb]);
                break;
            case infoType.KnockBack:
                myText.text = string.Format("{0:F0}", ItemController.Instance.items[(int)Item.KnockBack]);
                break;
            case infoType.Retry:
                myText.text = string.Format("{0:F0}", GameManager.Instance.currentStage);
                break;
        }
    }

}
