using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeOneTrigger3 : MonoBehaviour {

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            Debug.Log("BridgeOne Event3 Start");
            MonsterManager.instance.BridgeOneMon(2, 1);
            this.gameObject.SetActive(false);
        }
    }
}
