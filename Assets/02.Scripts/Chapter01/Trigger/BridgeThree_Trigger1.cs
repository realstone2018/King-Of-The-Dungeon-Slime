using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeThree_Trigger1 : MonoBehaviour {
    public GameObject AoeCyan;
    public GameObject AoeMagenta;
    public GameObject AoeYellow;
    public GameObject AoeMagenta2;
    public GameObject AoeYellow2;
    public GameObject AoeCyan2;
    public GameObject AoeYellow3;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "SLIME_CYAN" || coll.tag == "SLIME_MAGENTA" || coll.tag == "SLIME_YELLOW")
        {
            Debug.Log("BridgeThree Event1 Start");
            StartCoroutine(BridgeThreeEventStream());

            FixCam.instance.FocusingTargetString("Slime_Cyan");
            FixCam.instance.DistanceChagne(-12f, 25.5f, -10f);
        }
    }

    IEnumerator BridgeThreeEventStream()
    {
        AoeCyan.SetActive(true);
        yield return new WaitForSeconds(5.0f);

        AoeMagenta.SetActive(true);
        AoeYellow.SetActive(true);
        yield return new WaitForSeconds(6.0f);

        AoeMagenta2.SetActive(true);
        AoeYellow2.SetActive(true);

        yield return new WaitForSeconds(6.0f);
        AoeCyan2.SetActive(true);
        AoeYellow3.SetActive(true);

    }
}
