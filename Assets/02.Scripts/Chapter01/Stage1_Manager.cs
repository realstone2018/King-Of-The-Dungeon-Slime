using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Manager : MonoBehaviour
{
    public Stage1_UIManager UIManager;
    public ElevatorGoUp elevator;

    void Start()
    {
        // 스테이지 흐름 코루틴 시작 
        StartCoroutine(Stream());
    }

    IEnumerator Stream()
    {
        UIManager.OpenStage();
        yield return new WaitForSeconds(3.5f);

        // 몬스터 세마리를 출력하기 
        MonsterManager.instance.TutorialOneMon();
        
        // 스테이지에 존재하는 몬스터의 수가 0이 될 때 까지 반복 
        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            CheckAndDestoryMonsterRGB();
            yield return new WaitForSeconds(1.0f);
        }

       // 튜토리얼용 패널 비활성화 
        UIManager.ClearTutorial();

        // 혼합 몬스터들을 일정시간마다 Find() 하여 Destroy() 한다
        StartCoroutine(WhileCheckAndDestoryMonsterRGB());

        elevator.StartMoveElevator(69.5f);
        yield return new WaitForSeconds(6.0f);

        MonsterManager.instance.CreateStart(2, "Stage1_Point");
        yield return new WaitForSeconds(10.0f);
        MonsterManager.instance.StopCreateMon();

        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            yield return new WaitForSeconds(1.0f);
        }

        elevator.StartMoveElevator(141.2f);

        yield return new WaitForSeconds(9.0f);
        FixCam.instance.FocusingTargetString("Slime_Cyan");
    }

    private void CheckAndDestoryMonsterRGB()
    {
        GameObject MonsterR = GameObject.Find("Monster_Red(Clone)");
        GameObject MonsterG = GameObject.Find("Monster_Green(Clone)");
        GameObject MonsterB = GameObject.Find("Monster_Blue(Clone)");
        GameObject MonsterBlack = GameObject.Find("Monster_Black(Clone)");

        // find함수는 객체를 찾지못하면 null값을 가진다.
        if (MonsterR != null || MonsterG != null || MonsterB != null || MonsterBlack != null)
        {
            // 아직 Stage1에선 혼합 개념을 배우지 않았으므로 없애버린다.
            Destroy(MonsterR);
            Destroy(MonsterG);
            Destroy(MonsterB);
            Destroy(MonsterBlack);
        }
    }

    IEnumerator WhileCheckAndDestoryMonsterRGB()
    {
        while (true)
        {
            CheckAndDestoryMonsterRGB();
            yield return new WaitForSeconds(2.0f);
        }
    }

    // Stage1이 끝나고 매니저 오브젝트가 비활성화 될 때 모든 코루틴 정지
    void OnDisable()
    {
        StopAllCoroutines();
    }
}
