using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondAOEeffect : MonoBehaviour {

    public GameObject diamondAOE;
    public GameObject range;
    public GameObject dust;
    public GameObject attackTrigger;

    void OnTriggerEnter(Collider coll)
    {
        //Charge가 effectTrigger에 닿으면  폭발트리거 생성 
        if (coll.gameObject.tag == "DIAMONDAOE_CHARGE")
        {
            StartCoroutine(Explosion(coll));
        }
    }


    IEnumerator Explosion(Collider charge)
    {
        //explosionTrigger.SetActive(true);
        charge.gameObject.SetActive(false);
        //Debug.Log("Explosion!");
        attackTrigger.SetActive(true);
        range.SetActive(false);
        // 투사체 낙하, 이펙트 
        dust.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        diamondAOE.SetActive(false);
    }
}
