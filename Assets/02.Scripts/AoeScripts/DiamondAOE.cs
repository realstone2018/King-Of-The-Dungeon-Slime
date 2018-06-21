using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondAOE : MonoBehaviour {

    public GameObject AOEcharge;        // 생성할 프리팹을 저장한 변수
    private GameObject charge;          // 생성한 프리팹을 저장할 변수
    public GameObject startOb;           // 프리팹 생성 지점 

    Quaternion chargeRot;
    public float speed = 0.01f;

    public int aoeColorId = 0;

    void Start()
    {
        chargeRot = startOb.transform.rotation;

        charge = (GameObject)Instantiate(AOEcharge, startOb.transform.position, chargeRot);

        charge.transform.parent = this.transform;

        // Charge의 크기를 Start의 크기만큼 변경 
        Vector3 startScale = startOb.transform.localScale;
        charge.transform.localScale = startScale;
    }

    void Update()
    {
        charge.transform.localScale += new Vector3(speed, speed, 0);
    }
}
