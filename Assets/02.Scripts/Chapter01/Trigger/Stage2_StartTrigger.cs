using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_StartTrigger : MonoBehaviour {
    public GameObject stage1_Manager;
    public GameObject stage2_Manager;

    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            Debug.Log("Stage2 Start Trigger Work");
            StartCoroutine(Step());
        }
    }

    IEnumerator Step()
    {

        MonsterManager.instance.DeleteAllMonster();

        stage1_Manager.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        stage2_Manager.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        this.gameObject.SetActive(false);
    }
}
