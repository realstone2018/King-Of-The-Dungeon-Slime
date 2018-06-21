using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        Debug.Log("Sphere ColiisionEnter()");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sphere TriggerEnter()");
    }

    void Update()
    {
        //this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(2, 0, 0));
    }
}
