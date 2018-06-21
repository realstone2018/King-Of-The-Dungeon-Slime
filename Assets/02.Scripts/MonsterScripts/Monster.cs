using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour {
    public Transform tr;
    public float power = 100.0f;

    //추적을 위한 NavmeshAgent 컴포넌트 변수 
    private NavMeshAgent nvAgent;
    //추적을 위한 Slime 좌표 계산 
    public Vector3 targetTr;

    // 타격시 상태변환을 위한 프리팹 저장
    public GameObject monster_Red;
    public GameObject monster_Green;
    public GameObject monster_Blue;
    public GameObject monster_Black;


    //이렇게 모든코드에서 쓰이는 변수는 어찌 하지  전역변수? 아니면 일반화시켜서 상속?
    public GameObject slime_C;
    public GameObject slime_M;
    public GameObject slime_Y;
    public GameObject slime_R;
    public GameObject slime_G;
    public GameObject slime_B;
    public GameObject slime_Black;
    // foreach문으로 처리하기 위한 리스트
    public List<GameObject> slimeList = new List<GameObject>();

    public bool monsterDeadBool= false;


    public AudioSource source = null;
    public AudioClip crashStone;

    void Start()
    {
        source = GetComponent<AudioSource>();


        slimeList.Add(slime_C = SelectUnit.instance.slime_C);
        slimeList.Add(slime_M = SelectUnit.instance.slime_M);
        slimeList.Add(slime_Y = SelectUnit.instance.slime_Y);
        slimeList.Add(slime_R = SelectUnit.instance.slime_R);
        slimeList.Add(slime_G = SelectUnit.instance.slime_G);
        slimeList.Add(slime_B = SelectUnit.instance.slime_B);
        slimeList.Add(slime_Black = SelectUnit.instance.slime_Black);


        //이 오브젝트의 NavMeshAgent를 변수에 저장 
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.GetComponent<NavMeshAgent>().enabled = true;
        //살아있다면 추적 코루틴 시작 
        StartCoroutine(this.Trace());
    }

    void Update()
    {
        //몬스터와 타겟의 위치는 계속변하므로 Update에서 해결 
        //이 오브젝트(몬스터)의 위치를 tr에 저장 
        tr = this.gameObject.GetComponent<Transform>();
        //가장 가까운 슬라임의 위치를 SlimeTr에 저장 
        targetTr = FindTarget().transform.position;

		if (!monsterDeadBool)
		{
			nvAgent.destination = targetTr;
			nvAgent.Resume();
		}
    }

    GameObject FindTarget()
    {
        GameObject closeTarget = null;
        float dist1 = 1000, dist2;

        // slimeTr과 각 슬라임들의 위치를 비교 
        foreach (GameObject slime in slimeList)
        {
            // slime이 활성화 상태일때만 비교 
            if (slime.activeSelf)
            {
                dist2 = Vector3.Distance(this.transform.position, slime.transform.position);
                //Debug.Log("현재 최단거리 : " + dist1 + "   " + slime + "까지의 거리 : " + dist2);

                if (dist2 < dist1)
                {
                    // 최단거리 갱신
                    dist1 = dist2;
                    // 해당 요소를 최단거리타겟으로 설정 
                    closeTarget = slime;
                }
            }
        }
        // slimeList를 모두 돌려보고 가장 가까운타겟을 반환
        //Debug.Log("최단거리 목표는 : " + closeTarget);
        return closeTarget;
    }

    IEnumerator Trace()
    {
        while (!monsterDeadBool)
        {
            nvAgent.destination = targetTr;
            nvAgent.Resume();

            yield return new WaitForSeconds(0.2f);
        }
    }

    // 색 별로 달라지는 지점(오버라이딩 필요)
    void OnCollisionEnter(Collision coll)
    {

    }


    // 몬스터를 초기화 후 오브젝트풀로 환원  
    protected void MonsterDead()
    {
        source.PlayOneShot(crashStone, 0.1f);

        monsterDeadBool = true;
        // 몬스터의 상태를 죽은상태로 
        //Debug.Log("crush");
        //총알 제거
        //Destroy(coll.gameObject);

        //죽는 애니메이션 대체 일단 날려버림 
        //StopCoroutine(Trace());  ???????????
        // 안날라가고 바닥에 붙어있는 현상 제거를 위해 nav컴포넌트를 꺼준다 .
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Rigidbody>().AddForce(transform.up * power);
        this.transform.rotation = Random.rotation;
        StartCoroutine(PushObjectPool());
    }

    protected void InvalidityAttack()
    {
        //총알 날라감 
    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);

        this.GetComponent<NavMeshAgent>().enabled = true;

        // 날라가던도중 비활성화 되었다가 활성화되면 마저 날라가는 현상 방지 
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        // 몬스터를 살아있는 상태로바꿈 
        monsterDeadBool = false;

        yield return null;
    }

    public void StopTrace()
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void OnEnable()
    {
        // 코루틴시작넣기, 절대강좌 참조 
    }
}
