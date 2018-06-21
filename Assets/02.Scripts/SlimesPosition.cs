using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesPosition : MonoBehaviour {
    // Scene에 존재하는 모든 슬라임을 담을 배열
    public GameObject[] slimes;
    // slimes배열 인자들의 position값을 담을 배열 
    public Vector3[] slimePosition;
    int idx = 0;

    // 싱글턴 변수
    public static SlimesPosition instance = null;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        FindSlime();
    }

    // 슬라임위치가 필요할때마다 다른 스크립트에서 호출될 용도의 함수 
    public Vector3[] FindSlime()
    {
        //Debug.Log("FindSlime()");
        idx = 0;

        foreach (GameObject slime in slimes)
        {
            //Debug.Log("FindSlime().foreach");
            // Debug.Log(slime.name + " : " + slime.activeSelf);

            // 슬라임이 활성화 되어있는 겨웅에만 Position배열에 집어넣는다.
            if (slime.activeSelf)
            {
                slimePosition[idx++] = slime.transform.position;
            }

            /* 에러 발생, slimePosition 배열의 [0]이 할당되자않은 인자라 나온다. 
             * public Vector3[] slimePosition;
             * int idx = 0;
             * 
             * if (slime.actvieSelf)
             * {
             *      slimePosition[idx++] = slime.tranform.position;    
             * }
             * */    
        } 
        return slimePosition;
    }

    // 배열 초기화
    public void ClearPosition()
    {
        slimePosition[0] = Vector3.zero;
        slimePosition[1] = Vector3.zero;
        slimePosition[2] = Vector3.zero;
    }   
}
