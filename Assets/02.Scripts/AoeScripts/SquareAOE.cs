using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAOE : MonoBehaviour {
    public GameObject AOEcharge;        // 생성하려는 프리팹을 저장한 변수
    private GameObject charge;          // 생성한 프리팹을 저장할 변수
    public GameObject startOb;           // 프리팹 생성 지점 
    public GameObject range;
    public GameObject explosionTrigger;
    public GameObject burst;

    public float moveSpeed = 100.0f;
    
    //프리팹 생성 시 Transform의 rotation값 
    Quaternion chargeRot = Quaternion.Euler(90, 0, 0);

    public int aoeColorId = 0;

    void Start ()
    {
        // AOEcharge프리팹을 startTr포지션에 chargeRot각도로 생성해서  charg 변수에 저장
        charge = (GameObject)Instantiate(AOEcharge, startOb.transform.position, chargeRot) as GameObject;

        // charge의 부모를 이 오브젝트로 
        charge.transform.parent = this.transform;

        // charge의  크기를 startOb 만큼
        Vector3 startScale = startOb.transform.localScale;
        charge.transform.localScale = startScale;

        StartCoroutine(ExplosionTimer());
    } 
	
    void Update()
    {
        charge.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(6.0f);
        explosionTrigger.SetActive(true);
        range.SetActive(false);
        charge.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        burst.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        // 효과음 
        // 이펙트 OR 투사체 StartOb에서 발사 (발사방식은 charge와 같게 ) 
    }


    /*
    void Update ()
    {
        // charge를 오른쪽(x+) 방향(월드좌표기준)으로 이동시켜라 
        StartCoroutine(this.CreateCharge());

    }


    IEnumerator CreateCharge()
    {
        yield return new WaitForSeconds(0.3f);
        while(true)
        {
            charge.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

	/*
	void Update () {
        Vector3 currScale = AOEcharge.localScale;
        AOEcharge.transform.localScale = new Vector3(charge, currScale.y, currScale.z);
        AOEcharge.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        charge += 0.01f;
	}
    */
}
