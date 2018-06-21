using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeOneTrigger4 : MonoBehaviour {

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            Debug.Log("BridgeOne Event4 Start");
            MonsterManager.instance.BridgeOneMon(0, 2);
            this.gameObject.SetActive(false);
        }
    }
}
