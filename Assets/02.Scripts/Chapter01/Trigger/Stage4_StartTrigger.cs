using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_StartTrigger : MonoBehaviour {
    public GameObject stage3_Manager;
    public GameObject stage4_Manager;

    public GameObject Canon1;

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            Destroy(Canon1);

            stage3_Manager.SetActive(false);
            stage4_Manager.SetActive(true);

            this.gameObject.SetActive(false);
        }

    }
}
