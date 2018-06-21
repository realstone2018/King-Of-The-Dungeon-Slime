using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

    public float speed = 10.0f;

	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        StartCoroutine(DestroyBullet());
    }
 
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

}
