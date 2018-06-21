using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStake : MonoBehaviour {
    /* Chapter2_Manager스크립트에서   DropStakePrefeb을 instantiate로 생성 시 
     * DropStake스크립트에서          stake를 떨어트리고 
     * ImpactPoint스크립트에서        impactPoint에 stake 충돌 시 효과음, 이펙트 활성화
    */
    public GameObject stake;
    public Vector3 targetPosition;
    public GameObject impactPoint;

    // start() 으로 하면 오류출력 안떠도 에러뜨니 조심  
    void Start()
    {
        //Debug.Log("DropStake_start()");
        StartCoroutine(StakeThrow());
        targetPosition = this.transform.position;
    }

    // 비석을 목표 낙하지점 상단으로 이동,  낙하지점으로 빠르게이동 
    IEnumerator StakeThrow()
    {
        yield return new WaitForSeconds(1.0f);

        //Debug.Log("StakeThrow()");
        Vector3 waitPosition = new Vector3(targetPosition.x + 180, targetPosition.y + 250, targetPosition.z);
        stake.transform.position = waitPosition;
        stake.SetActive(true);

        while (stake.transform.position.y >= 25)
        {
            //Debug.Log(stake.transform.position.y);
            stake.transform.position = Vector3.MoveTowards(stake.transform.position, targetPosition, 10);
            yield return new WaitForSeconds(0.0001f);
        }
        impactPoint.SetActive(false);

        yield return null;
    }
}
