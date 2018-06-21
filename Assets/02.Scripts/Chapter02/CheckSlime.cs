using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSlime : MonoBehaviour {
    // 트리거오브젝트에 들어갈 스크립트,  트리거 내 슬라임이 있을 시 트루 반환
    public bool check = false;

    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("일단 충돌");
        if (coll.gameObject.tag == "SLIME_CYAN" || coll.gameObject.tag == "SLIME_MAGENTA" || coll.gameObject.tag == "SLIME_YELLOW")
        {
            Debug.Log(this.gameObject + "Operation");
            check = true;
            // 매니저에게 알려야함  
            this.gameObject.SetActive(false);
        }


    }


}
