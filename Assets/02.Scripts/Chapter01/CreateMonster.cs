using System.Collections;
using UnityEngine;
// List 자료형을 사용하기 위해 추가해야 하는 네임스페이스 
using System.Collections.Generic;


public class MonsterManager : MonoBehaviour
{
    //몬스터가 출현할 위치를 담을 배열 
    public Transform[] points;
    public Transform tutorialPoint1;
    public Transform tutorialPoint2;
    public Transform tutorialPoint3;
    public Transform tutorialTwoPoint1;
    //public Transform tutorialTwoPoint2;
    //public Transform tutorialTwoPoint3;
    // 생성할 몬스터들의 프리팹 배열 
    public GameObject[] monsterPrefab;
    // 몬스터를 미리 생성해 저자알 리스트 자료형 
    public List<GameObject> monsterPool = new List<GameObject>();


    //몬스터 발생 주기 
    public float createTime = 6.0f;
    //몬스터의 최대 발생 개수
    public int maxMonster = 5;
    //게임 종료 여부 변수, 플레이어가 죽었을때, 게임이 종료됬을때 다른 스크립트에서 접근 
    public bool isGameOver = false;
    //싱글턴 패턴을 위한 인스턴스 변수 선언 
    public static MonsterManager instance = null;

    public GameObject tutorialMonster1;
    public GameObject tutorialMonster2;
    public GameObject tutorialMonster3;
    public GameObject tutorialMonster4;
    //public GameObject tutorialMonster5;
    //public GameObject tutorialMonster6;

    public bool stopCreate = false;

    void Awake()
    {
        //Stage1_Mgr 클래스를 인스턴스에 대입 
        instance = this;
    }

    public void CreateStart(int monsterlevel, string pointName)
    {
        //CreateStart 함수가 호출되었을땐 풀어주기 
        stopCreate = false;

        // Hierarchy 뷰의 pointName 이름의 오브젝트를 찾아 하위(Children)에 있는 모든(Components) Trasform 컴포넌트를 찾아옴 
        // 이때 points 배열에 SpawnPoint의 Transform도 들어가기때문에 Ran인자로 (1, Length)를 준다.
        points = GameObject.Find(pointName).GetComponentsInChildren<Transform>();

        //몬스터를 6마리 미리 생성해 오브젝트 풀에 저장 
        for (int i = 0; i < maxMonster; i++)
        {
            //생성할 몬스터의 종류 랜덤   
            int ran = Random.Range(0, monsterlevel);
            //몬스터 프리팹을 생성 
            GameObject monster = (GameObject)Instantiate(monsterPrefab[ran]);
            //생성한 몬스터의 이름 설정
            monster.name = "Monster_" + i.ToString();
            //생성한 몬스터를 비활성화
            monster.SetActive(false);

            //생성한 몬스터를 오브젝트 풀에 추가 
            monsterPool.Add(monster);
        }

        if (points.Length > 0)      // 출현할 위치가 할당된 상태라면 
        {
            //몬스터 생성 코루틴 함수 호출
            StartCoroutine(CreateMon());
        }
    }

    /* 오브젝트풀 없이 생성하는 방법
    IEnumerator CreateMonster()
    {
        //게임 종료 시까지 무한 루프 
        while ( !isGameOver )
        {
            //현재 생성된 몬스터 개수 산출 ,   ??? Length를 이용해서 게임오브젝트 수 카운트 가능 
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("MONSTER").Length;
            //몬스터의 최대 생성 개수보다 작을 때만 몬스터 생성 
            if (monsterCount < maxMonster)
            {
                //몬스터의 생성 주기 시간만큼 대기 
                yield return new WaitForSeconds(createTime);
                //생성할 몬스터의 종류 랜덤   
                int ran = Random.Range(0, monsterPrefab.Length);
                //불규칙적인 위치 산출 
                int idx = Random.Range(1, points.Length);
                //몬스터의 동적 생성 
                Instantiate(monsterPrefab[ran], points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
            
            }
        }
        */

    //오브젝트풀을 이용한 몬스터 생성 코루틴 함수 
    IEnumerator CreateMon()
    {
        //게임 종료 시까지 무한 루프 
        while (!isGameOver && !stopCreate)
        {
            // 몬스터 생성 주기 시간만큼 메인 루프에 양보 
            yield return new WaitForSeconds(createTime);

            //플레이어가 사망했을 때 코루틴을 종료해 다음 루틴을 진행하지 않음 
            if (isGameOver) yield break;

            //오브젝트 풀의 처음부터 끝까지 순회
            foreach (GameObject monster in monsterPool)
            {
                //비활성화 여부로 사용 가능한 몬스터를 판단 
                // ★ 오브젝트가 활성화되있는지 여부 판단 함수  .activeSelf
                // monsterPool의 모든monster가 active상태이면 생성되지 않으니 수 제한 둘필요 x 
                // 혼합되는 몬스터까지 계산해서 10마리까지만 소환

                if (!monster.activeSelf)
                {
                    //몬스터를 출현시킬 위치의 인덱스 값을 추출
                    int idx = Random.Range(1, points.Length);
                    //몬스터의 출현위치를 설정
                    monster.transform.position = points[idx].position;
                    //몬스터를 활성화함
                    monster.SetActive(true);
                    //오브젝트 풀에서 몬스터 프리팹 하나를 활성화한 후 for 루프를 빠져나감 
                    break;
                }
            }
        }
    }

    public void StopCreateMon()
    {
        stopCreate = true;
        StopCoroutine(CreateMon());
        
        monsterPool.Clear();
        
    }

    public void TutorialOneMon()
    {
        tutorialMonster1 = (GameObject)Instantiate(monsterPrefab[0], tutorialPoint1.position, tutorialPoint1.rotation);
        tutorialMonster1.GetComponent<Monster>().StopTrace();
        tutorialMonster2 = (GameObject)Instantiate(monsterPrefab[1], tutorialPoint2.position, tutorialPoint2.rotation);
        tutorialMonster2.GetComponent<Monster>().StopTrace();
        tutorialMonster3 = (GameObject)Instantiate(monsterPrefab[2], tutorialPoint3.position, tutorialPoint3.rotation);
        tutorialMonster3.GetComponent<Monster>().StopTrace();
    }

    public void TutorialOneFalse()
    {
        Destroy(tutorialMonster1);
        Destroy(tutorialMonster2);
        Destroy(tutorialMonster3);
    }

    public void TutorialTwoRed()
    {
        tutorialMonster4 = (GameObject)Instantiate(monsterPrefab[3], tutorialTwoPoint1.position, tutorialTwoPoint1.rotation);
        tutorialMonster4.transform.position = tutorialTwoPoint1.position;
        tutorialMonster4.GetComponent<Monster>().StopTrace();

    }
    /*
    public void TutorialTwoGreen()
    {
        tutorialMonster5 = (GameObject)Instantiate(monsterPrefab[4], tutorialTwoPoint2.position, tutorialTwoPoint2.rotation);
        tutorialMonster5.transform.position = tutorialTwoPoint2.position;
        tutorialMonster5.GetComponent<Monster>().StopTrace();
    }

    public void TutorialTwoBlue()
    {
        tutorialMonster6 = (GameObject)Instantiate(monsterPrefab[5], tutorialTwoPoint3.position, tutorialTwoPoint3.rotation);
        tutorialMonster6.transform.position = tutorialTwoPoint3.position;
        tutorialMonster6.GetComponent<Monster>().StopTrace();
    }
    */
    public void TutorialTwoFalse()
    {
        Destroy(tutorialMonster4);
        //Destroy(tutorialMonster5);
        //Destroy(tutorialMonster6);

    }

    public void BridgeOneMon(int num, int mode)
    {
        if (mode == 1)
        {
            points = GameObject.Find("BridgePoint" + num.ToString()).GetComponentsInChildren<Transform>();

            foreach (Transform point in points)
            {
                GameObject monster = (GameObject)Instantiate(monsterPrefab[num], point.position, point.rotation);
                //생성한 몬스터의 이름 설정
                monster.name = "Monster_" + point.ToString();
            }
        }
        else if (mode == 2)
        {
            points = GameObject.Find("BridgePoint" + (num + 3).ToString()).GetComponentsInChildren<Transform>();

            foreach (Transform point in points)
            {
                GameObject monster = (GameObject)Instantiate(monsterPrefab[num], point.position, point.rotation);
                //생성한 몬스터의 이름 설정
                monster.name = "Monster_" + point.ToString();
            }

            points = GameObject.Find("BridgePoint" + (num + 4).ToString()).GetComponentsInChildren<Transform>();

            foreach (Transform point in points)
            {
                GameObject monster = (GameObject)Instantiate(monsterPrefab[num+1], point.position, point.rotation);
                //생성한 몬스터의 이름 설정
                monster.name = "Monster_" + point.ToString();
            }
        }
    }

    public void DeleteAllMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        foreach(GameObject monster in monsters)
        {
            if (monster != null)
            {
                Destroy(monster);
            }
        }
    }


    public void BossStageMon()
    {
        //CreateStart 함수가 호출되었을땐 풀어주기 
        stopCreate = false;

        // Hierarchy 뷰의 SpawnPoint를 찾아 하위(Children)에 있는 모든(Components) Trasform 컴포넌트를 찾아옴 
        // 이때 points 배열에 SpawnPoint의 Transform도 들어가기때문에 Ran인자로 (1, Length)를 준다.
        points = GameObject.Find("Boss_Point").GetComponentsInChildren<Transform>();

        //몬스터를 6마리 미리 생성해 오브젝트 풀에 저장 
        for (int i = 0; i < 3; i++)
        {
            //생성할 몬스터의 종류 랜덤   
            int ran = Random.Range(0, 3);
            //몬스터 프리팹을 생성 
            GameObject monster = (GameObject)Instantiate(monsterPrefab[ran]);
            //생성한 몬스터의 이름 설정
            monster.name = "Monster_" + i.ToString();
            //생성한 몬스터를 비활성화
            monster.SetActive(false);

            //생성한 몬스터를 오브젝트 풀에 추가 
            monsterPool.Add(monster);
        }

        if (points.Length > 0)      // 출현할 위치가 할당된 상태라면 
        {
            //몬스터 생성 코루틴 함수 호출
            StartCoroutine(CreateMon());
        }
    }

    public int CheckMonsterNum()
    {
        int monsterNum = 0;

        monsterNum = GameObject.FindGameObjectsWithTag("MONSTER").Length;

        return monsterNum;
    }

}

