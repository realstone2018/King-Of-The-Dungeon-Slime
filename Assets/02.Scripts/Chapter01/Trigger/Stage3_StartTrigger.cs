using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_StartTrigger : MonoBehaviour
{
    public GameObject stage2_Manager;
    public GameObject stage3_Manager;

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            stage2_Manager.SetActive(false);
            stage3_Manager.SetActive(true);

            this.gameObject.SetActive(false);
        }

    }
}