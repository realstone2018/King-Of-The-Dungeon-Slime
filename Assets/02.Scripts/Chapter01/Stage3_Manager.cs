using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Manager : MonoBehaviour {

    public ElevatorGoUp elevator;

    public GameObject stage3_Center;
    public GameObject[] aoes;
    
    public int MonsterNum;

    void Start()
    {
        StartCoroutine(Stream());
    }

    IEnumerator Stream()
    {
        FixCam.instance.FocusingTarget(stage3_Center);
        yield return new WaitForSeconds(0.1f);
        FixCam.instance.DistanceChagne(24f, 20f, 10f);

        MonsterManager.instance.CreateStart(4, "Stage3_Point");
        yield return new WaitForSeconds(12.0f);
        MonsterManager.instance.StopCreateMon();

        while (MonsterManager.instance.CheckMonsterNum() != 0)
        {
            yield return new WaitForSeconds(3.0f);
        }

        elevator.StartMoveElevator(428.42f);

        foreach (GameObject aoe in aoes)
        {
            aoe.SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }

        FixCam.instance.FocusingTargetString("Slime_Cyan");
        FixCam.instance.DistanceChagne(6.0f, 43.5f, 25f);
    }
}
