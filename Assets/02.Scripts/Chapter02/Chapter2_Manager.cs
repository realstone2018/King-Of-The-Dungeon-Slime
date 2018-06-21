using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_Manager : MonoBehaviour {

    public CheckSlime stageOne;
    public CheckSlime stageTwo;
    public CheckSlime stageThree;

    public GameObject[] graves;
    public Transform[] points;
    public int graveCnt;
    int idx = 1;

    public GameObject statue;
    public GameObject statueAOE;

    public GameObject dropStakePrefeb;
    public GameObject[] dropStake;

    public Chapter2_UIManager UIManager;
    public static Chapter2_Manager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        points = GameObject.Find("Point").GetComponentsInChildren<Transform>();
        //◆ startStake = stake.transform.position;

        //오프닝 UI 출력 
        UIManager.StartOpenning();
    
    }

    void Update()
    {
        /*◆
        if (stake.transform.position.y >= 50)
        {
            StakeThrow(new Vector3(0, 0, 0));
        }
        */

        if (stageOne.check)
        {
            StartCoroutine(StageOneStream());
            stageOne.check = false;
        }
        else if (stageTwo.check)
        {
            StartCoroutine(StageTwoStream());
            stageTwo.check = false;

        }
        else if (stageThree.check)
        {
            StartCoroutine(StageThreeStream());
            stageThree.check = false;

        }
    }

    IEnumerator StageOneStream()
    {

        // 무덤 4개가 활성화  (5초에 한번씩 유령 소환)
        // 몬스터는 무덤이 알아서 생성 
        // Stage에 무덤이 모두 없어지면 스테이지 클리어  
        // 부모가 0번째 인덱스에 들어가므로 1부터 시작 

        // points[1] 부터 [4]까지에  무덤 배치 후 생성
        foreach (GameObject grave in graves)
        {
            Debug.Log("idx : " + idx);
            if (idx <= 4)
            {
                grave.transform.position = points[idx++].position;
                grave.GetComponent<Grave>().Appear();
            }
        }

        // 스테이지 클리어 grave가 활성화될때 +1, 비활성화 될때 -1 값조절, 어느스크립트에서?
        //int num = GameObject.FindGameObjectsWithTag("GRAVE").Length;
        while (graveCnt != 0)
        {
            // 대기
            // 무덤들을 무시하고 지나가는걸 방지하기위한 벽 설치, 무덤이 0이 될시 사라짐 
            yield return new WaitForSeconds(1.0f);
        }


        ///////////////  Stage1 Clear  ////////////////

        yield return null;
    }


    IEnumerator StageTwoStream()
    {
        DeleteAllGhost();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ThrowStakeOneByOne());

        // 포인트 5부터 8까지 c,m,y,r 무덤 소환 
        foreach (GameObject grave in graves)
        {
            Debug.Log("idx : " + idx);
            if (idx <= 7)
            {
                grave.transform.position = points[idx++].position;
                grave.GetComponent<Grave>().Appear();
            }
        }
        idx++;

        yield return new WaitForSeconds(6.0f);
        int count = 0;

        // 포인트 9 ~ 12 까지 c2,m2,y2, g 무덤 소환 
        foreach (GameObject grave in graves)
        {
            // 이미 활성화되어있을 cmy 무덤(0~2)은 넘기고 3부터 시작 
            if (count > 2)
            {
                if (idx <= 11)
                {
                    grave.transform.position = points[idx++].position;
                    grave.GetComponent<Grave>().Appear();
                }
            }

            count++;
        }
        idx++;
        yield return new WaitForSeconds(6.0f);
        count = 0;

        // 포인트 13 ~ 16까지 c3, m3, y3, b 무덤 소환 
        foreach (GameObject grave in graves)
        {
            // 이미 활성화되어있을 cmy, cmy2 무덤(0~5)은 넘기고 3부터 시작 
            if (count > 5)
            {
                if (idx <= 15)
                {
                    grave.transform.position = points[idx++].position;
                    grave.GetComponent<Grave>().Appear();
                }
            }
            count++;
        }

        yield return null;
    }


    IEnumerator StageThreeStream()
    {
        DeleteAllGhost();
        yield return new WaitForSeconds(0.5f);

        // statue의 hp가 0이 되면 이벤트발생, aoe가 사라진다.
        Statue.OnDestroyStatue += OnDestroyStatue;

        // 포인터 17 18 19 에 cmy무덤 생성 
        foreach (GameObject grave in graves)
        {
            if (idx <= 19)
            {
                grave.transform.position = points[idx++].position;
                grave.GetComponent<Grave>().Appear();
            }

        }
        yield return new WaitForSeconds(10.0f);

        // 일정 시간이 지난후 중앙 비석에 HP생성, 타격 트리거 발동
        statue.GetComponent<Statue>().AppearHpbar();
        statue.GetComponent<Statue>().setStatue = true;

        // 검은색 aoe 생성, 비석 hp가 0이되면 aoe 사라짐  
        statueAOE.SetActive(true);



        yield return null;
    }

    void OnDestroyStatue()
    {
        statueAOE.SetActive(false);
    }

    IEnumerator CreateStakeAtOnce(Vector3[] slimePosition)
    {
        //Debug.Log("CreateStake()");

        /* ◆
        Vector3 middlePosition = (startStake + targetPosition) / 2.0f;
        Vector3 anglePosition = new Vector3(middlePosition.x+ 100, middlePosition.y, middlePosition.z);

        stake.transform.RotateAround(anglePosition, Vector3.forward, 5);
        Debug.Log(middlePosition);
        //middlePosition을 중심으로 말뚝 회전, middlePosition을 중심으로 z축 회전밖에안되므로   z축, y축을 동시에 반영할 Vector3가 필요 => 보류
        */
        int i = 0;

        // 말뚝 생성 후 각 슬라임 위치로 위치 조정 
        foreach (Vector3 targetPosition in slimePosition)
        {
            //Debug.Log("target " + i + " : " +targetPosition); 
            dropStake[i] = (GameObject)Instantiate(dropStakePrefeb);
            dropStake[i++].transform.position = new Vector3(targetPosition.x, 0, targetPosition.z);

           // yield return new WaitForSeconds(0.8f);
        }
        yield return null;
        // 이펙트, 사운드 출력 
    }

    IEnumerator CreateStakeOneByOne()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("순차드랍 "); 
            dropStake[i] = (GameObject)Instantiate(dropStakePrefeb);
            dropStake[i].transform.position = new Vector3(SlimesPosition.instance.slimePosition[i].x, 0, SlimesPosition.instance.slimePosition[i].z);

            yield return new WaitForSeconds(0.8f);
        }
    }


    IEnumerator ThrowStakeAtOnce()
    {
        while(true)
        {
            yield return new WaitForSeconds(8.0f);
            StartCoroutine(CreateStakeAtOnce(SlimesPosition.instance.FindSlime()));
        }
    }

    IEnumerator ThrowStakeOneByOne()
    {
        while (true)
        {
            yield return new WaitForSeconds(8.0f);
            StartCoroutine(CreateStakeOneByOne());
        }
    }

    public void DeleteAllGhost()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("GHOST");

        foreach (GameObject monster in monsters)
        {
            if (monster != null)
            {
                Destroy(monster);
            }
        }
    }
}
