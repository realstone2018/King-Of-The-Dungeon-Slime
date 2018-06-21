using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    private void Update()
    {
        Debug.Log(gameObject.GetInstanceID());
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Cube ColiisionEnter()");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cube TriggerEnter()");
    }
}
