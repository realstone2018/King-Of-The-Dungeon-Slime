using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEeffect : MonoBehaviour {

    public Transform startTr;

    // SquareAOE, ArrowAOE 구분하여 구성, 확장시 편의를 위해 
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "SQUAREAOE_CHARGE")
        {
            coll.transform.position = startTr.position;
        }
        else if (coll.tag == "SQUAREAOE_ARROW")
        {
            coll.transform.position = startTr.position;
            // coll.gameObject.SetActive(false);
        }
    }
}
