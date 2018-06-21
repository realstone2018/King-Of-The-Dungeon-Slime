using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_Manager : MonoBehaviour {

    public ElevatorGoUp elevator;

    public GameObject stage4_Center;
    public GameObject[] aoes;
    
    public int monsterNum;

    void Start()
    {
        StartCoroutine(Stream());
    }

    IEnumerator Stream()
    {
        FixCam.instance.FocusingTarget(stage4_Center);

        yield return new WaitForSeconds(5.0f);
        elevator.StartMoveElevator(502.5f);

        for(int i = 0; i < 2; i++) 
        {
            aoes[i].SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }

        MonsterManager.instance.CreateStart(3, "Stage4_Point");

        yield return new WaitForSeconds(2.0f);
        for (int i = 2; i < 4; i++)
        {
            aoes[i].SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }

        MonsterManager.instance.StopCreateMon();

        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            yield return new WaitForSeconds(3.0f);
        }

        elevator.StartMoveElevator(574.4f);

        yield return new WaitForSeconds(0.5f);
        for (int i = 4; i < 6; i++)
        {
            aoes[i].SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }

        yield return new WaitForSeconds(1.0f);
        FixCam.instance.FocusingTargetString("Slime_Cyan");
		FixCam.instance.DistanceChagne(6.0f, 43.5f, 25f);
	}
}
