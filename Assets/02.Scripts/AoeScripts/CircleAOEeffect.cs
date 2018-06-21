using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAOEeffect : MonoBehaviour {
    
    public GameObject explosionTrigger;
    public GameObject circleAOE;
    public GameObject range;
    public GameObject DropStone;
    public GameObject dust;

	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CIRCLEAOE_CHARGE")
        {
            StartCoroutine(Explosion(coll));
        }
    }

    IEnumerator Explosion(Collider charge)
    {
        explosionTrigger.SetActive(true);
        charge.gameObject.SetActive(false);
        range.SetActive(false);

        DropStone.SetActive(true);
        DropStone.GetComponent<Rigidbody>().AddForce(Vector3.down * 100f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.55f);

        dust.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        circleAOE.SetActive(false);
    }
}
