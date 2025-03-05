using System.Collections;
using UnityEngine;

public  class Effect : MonoBehaviour
{
    public GameObject[] effects;
    int i = 0;

    private void Update()
    {
        StartCoroutine(Test());
    }

    //테스트
    IEnumerator Test()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            effects[i % 4].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            effects[i % 4].SetActive(false);
            i++;
        }
    }

    //i번째 게임오브젝트 활성화
    //이펙트 비활성화는 총알이 삭제되는 걸로
    public void PlayEffect(int i)
    {
        if (i >= effects.Length) return;
        
        effects[i].SetActive(true);
    }
}
