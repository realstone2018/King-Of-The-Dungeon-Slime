using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineLight : MonoBehaviour {

	void Update () {
        //this.transform.Rotate(Vector3.right * Time.deltaTime);
        this.transform.rotation = Random.rotation;
	}
}
