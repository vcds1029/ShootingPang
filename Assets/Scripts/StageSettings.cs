using UnityEngine;

public class StageSettings : MonoBehaviour
{
    //[SerializeField] private int itemBomb;      // ��ź: ���� ���� �� �� �ı�
    //[SerializeField] private int itemMagnet;    // �ڼ�: ���� ���� �� ������ ����
    //[SerializeField] private int itemKnockBack; // ��ġ��: ���� ���� �� �� ��ġ��
    //[SerializeField] private int itemImotal;    // ����: Ư�� ��� ȸ��
    //[SerializeField] private int itemCleaner;   // ������: Ư�� ��� ȸ��
    //[SerializeField] private int itemZeroGravity;   // ���߷�: Ư�� ��� ȸ��

    [SerializeField] private int[] items = new int[4];

    [SerializeField] private int numCoin;
    [SerializeField] private int numBullet;

    [SerializeField] private Vector3 playerPosition;

    [SerializeField] private int cameraSize;


    private void Awake()
    {
        SetStage();
    }

    private void SetStage()
    {
        ItemController.Instance.InitItem(items);

        //GameManager.Instance.remainCoin = numCoin;
        GameManager.Instance.InitCoin(numCoin);

        //PlayerController.Instance.bulletPossess = numBullet;
        PlayerController.Instance.InitBullet(numBullet);

        //PlayerController.Instance.gameObject.transform.position = PlayerPosition;
        PlayerController.Instance.InitPosition(playerPosition);

        Camera.main.orthographicSize = cameraSize;
    }

    public int GetCoinNum()
    {
        return numCoin;
    }
    
    public int GetBulletNum()
    {
        return numBullet;
    }
}
