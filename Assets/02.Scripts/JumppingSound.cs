using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 부모오브젝트의 콜라이더는 애니메이션에 따라 움직이지 않으므로, 따로 작성하여 자식한테 넣음 
public class JumppingSound : MonoBehaviour {

    public AudioSource source;
    public AudioClip jumpping;

    public bool wait = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
        wait = true;
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 14 && wait)
        {
            //Debug.Log("Collider 14 ");
            source.PlayOneShot(jumpping, 0.05f);
        }
    }

}
