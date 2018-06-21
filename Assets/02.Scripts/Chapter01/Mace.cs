using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour {
    public Transform tr;
    public int damage = 5;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "SLIME_CAYN" || coll.gameObject.tag == "SLIME_MAGENTA" || coll.gameObject.tag == "SLIME_YELLOW")
        {
            coll.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 5f, ForceMode.Impulse);
            SlimeHp.instance.DecreaseHp(damage);
        }
    }


}
