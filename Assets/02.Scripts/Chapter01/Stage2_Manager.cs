using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Manager : MonoBehaviour {

    public Stage2_UIManager UIManager;
    public ElevatorGoUp elevator;

    public GameObject Stage2_Center;
    
    public int MonsterNum;

    void Start()
    {
        StartCoroutine(Stream());
    }
    
    IEnumerator Stream()
    {
        FixCam.instance.FocusingTarget(Stage2_Center);

        // 혼합 키, 개념 설명 UI 출력 
        UIManager.TutorialTwoRedUI();

        MonsterManager.instance.TutorialTwoRed();

        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            //Find함수는 대상을 찾을 수 없으면 null을 반환, 만약 잘못눌러 색이 섞여 진행이 불가능한 경우 false처리 없애고 다시시작
            if ((GameObject.Find("Monster_Black(Clone)") != null))
            {
                Destroy(GameObject.Find("Monster_Black(Clone)"));
                // 초기화
                MonsterManager.instance.TutorialTwoFalse();
                UIManager.FalseTutorialTwo();
                MonsterManager.instance.TutorialTwoRed();
            }
            yield return new WaitForSeconds(1.0f);
        }

        UIManager.ClearTutorial();

        // 튜토리얼 1 클리어시 엘레베이터가 중간층으로 이동
        elevator.StartMoveElevator(214.6f);
        yield return new WaitForSeconds(7.5f);

        MonsterManager.instance.CreateStart(3, "Stage2_Point");
        yield return new WaitForSeconds(12.0f);
        MonsterManager.instance.StopCreateMon();

        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            yield return new WaitForSeconds(2.0f);
        }

        elevator.StartMoveElevator(286.6f);
        yield return new WaitForSeconds(10.0f);
        FixCam.instance.FocusingTargetString("Slime_Cyan");
        FixCam.instance.DistanceChagne(6.0f, 43.5f, 25f);
    }
}
