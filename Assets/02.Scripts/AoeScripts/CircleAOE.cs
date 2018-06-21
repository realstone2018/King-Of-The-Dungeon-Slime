using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAOE : MonoBehaviour {

    public  GameObject AOEcharge;      
    private GameObject charge;        
    public  GameObject startOb;         
    
    Quaternion chargeRot = Quaternion.Euler(0, 0, 0);
    public float speed = 0.05f;

    public int aoeColorId = 0;

    void Start()
    {
        charge = (GameObject)Instantiate(AOEcharge, startOb.transform.position, chargeRot);
        
        charge.transform.parent = this.transform;

        Vector3 startScale = startOb.transform.localScale;
        charge.transform.localScale = startScale;
    }

    // 원 형태의 charg를 중심점을 기준으로 점점 커지게 
    void Update()
    {
        charge.transform.localScale += new Vector3(speed, 0, speed);
    }

}
