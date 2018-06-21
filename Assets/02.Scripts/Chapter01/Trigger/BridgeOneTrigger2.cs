using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeOneTrigger2 : MonoBehaviour {

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            Debug.Log("BridgeOne Event2 Start");
            MonsterManager.instance.BridgeOneMon(1, 1);
            this.gameObject.SetActive(false);
        }
    }
}
