using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAOE : MonoBehaviour
{
    public GameObject AOEcharge;        // 생성하려는 프리팹을 저장한 변수 
    public GameObject charge;           // 생성한 프리팹을 저장할 변수
    public List<GameObject> chargeList = new List<GameObject>(); // 프리팹들을 묶는 리스트
    public GameObject startOb;           // 프리팹 생성 지점 

    public GameObject explosionTrigger;
    public bool explosion = false;

    public float moveSpeed = 35.0f;
    //프리팹 생성 시 Transform의 rotation값 
    Quaternion chargeRot;

    //Square AOE 때처럼 크기를 자동으로 맞출수 없으니 수동으로 바꿔주기 
    public Vector3 chargeScale = new Vector3(42, 1, 30);

    // aoeAct 값 변경은 STage Manager 스크립트에서 
    public bool aoeAct = true;
    public float createTime = 0.3f;
    public float repeatTime = 2.0f;

    public GameObject range;
    public GameObject burst;

    public int aoeColorId = 0;

    void Start()
    {
        StartCoroutine(ExplosionTimer());
        StartCoroutine(CreateCharge());
    }

    // chargeList들의 인자가 null이 아닐 때 전진 
    void Update()
    {
        foreach (GameObject charge in chargeList)
        {
            if (charge != null)
            {
                charge.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
    }

    // 4.5초 후 Trigger 활성화, 폭발 이펙트 활성화, 잠시 후 전부 비활성화  
    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(4.5f);
        explosionTrigger.SetActive(true);
        range.SetActive(false);
        foreach (GameObject charge in chargeList)
        {
            if (charge != null)
            {
                charge.SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.08f);
        burst.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        // 효과음 
        // 이펙트 OR 투사체 StartOb에서 발사 (발사방식은 charge와 같게 ) 
    }

    IEnumerator CreateCharge()
    {
        for (int i = 0; i < 5; i++)
        {
            Quaternion parentRot = transform.rotation;
            //chargeRot = Quaternion.Euler(0, -90, 0);
            //Quaternion rot = Quaternion.Lerp(chargeRot, partentRot, 10000) ;

            charge = (GameObject)Instantiate(AOEcharge, startOb.transform.position, parentRot) as GameObject;
            charge.transform.parent = this.transform;

            // charge의 크기 수동 조절 
            charge.transform.localScale = chargeScale;

            chargeList.Add(charge);
            yield return new WaitForSeconds(createTime);
        }
    }
}

/*  instantiate 반복과 effect부분 닿을 시 Destroy 로  오브젝트를 계속 생성, 삭제하니 
 *  부모객체의 방향이 바뀌어도 0, -90, 0으로 생성되어 어긋나는 현상 발생 
 *  객체를 처음에 생성하고 삭제하지 않고 계속 돌려쓰면 부모객체가 rotation해도 같이 rotation되어 이상 무 
 
     부모객체가 처음부터 rotation값을 가지면 같은 현상, 결국 부모 로테이션값을 계산해서 넣어야 할듯
     
     처음부터 오브젝트풀로 만들었으면 이런 에러 없는데 
     transfrom.rotation 값을 log해보면  유니티 인스펙터창과 다르다 .  => 쿼터니언값 !!!!
     */ 
