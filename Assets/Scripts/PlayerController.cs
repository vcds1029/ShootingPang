using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameObject BulletPrefab;


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지됨
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 삭제
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.identity);
    }

    public void MakeIt()
    {
        Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.identity);
    }
}