using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCam : MonoBehaviour {
    // 카메라가 바라보는 대상
    public Transform targetTr;

    // 카메라가 대상을 바라보는 거리, 높이, 각도  
    public float dist = 20.0f;
    public float height = 15.0f;
    public float dampTrace = 20.0f;



    private Transform tr;
    public int speed = 45;
    public float w = 0.0f;
    public float wheel = 1.2f;

    public Vector3 mousePosition;
    public Vector3 clickPosition;
    public Vector3 dir;

    public bool followMode = false;
    public float basicX = 24; public float basicY = 20; public float basicZ = 10;

    public static FixCam instance = null;

    void Awake()
    {
        instance = this;
    }


    void Start () {
        tr = GetComponent<Transform>();

        // 카메라의 위치 =  타겟의위치 - (dist, 0, 0) + (0,  height,  0) 
        tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) +
                  (Vector3.up * height), Time.deltaTime * dampTrace);

		//FixCam.instance.DistanceChagne(6.0f, 43.5f, 25f);

	}


	void Update ()
    {
        Vector3 followPosition = new Vector3(targetTr.position.x + basicX, targetTr.position.y + basicY, targetTr.position.z + basicZ);
        tr.position = Vector3.MoveTowards(tr.position, followPosition, Time.deltaTime * dampTrace);


        // 휠 up&down에 따른 카메라 확대, 축소 
        w = Input.GetAxis("Mouse ScrollWheel");
        // wheel의값이 0.8 ~ 1.8 외의 값을 가질수 없게 한다.
        wheel = Mathf.Clamp(wheel, 0.8f, 1.8f);
        
        // 휠을 아래로 굴릴때 -값,  위로 굴리면 +값 
        if (w < 0)
        {
            //★ Translate는 기본적으로 local좌표값으로 이동, 그래서 back만해도 xyz값이 변경되었던것
            tr.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else if(w > 0)
        {
            tr.Translate(Vector3.back * (-speed) * Time.deltaTime);
        }
        //Debug.Log("w : " + w + " wheel : " + wheel);
        


        // 휠 클릭 상태로 마우스 커서 이동에 따른 카메라 위치 이동 
        if (Input.GetMouseButtonDown(2))
        {
            // 휠 클릭 시 처음 값을 저장 
            clickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            // 휠을 눌른상태로 마우스 커서 이동시의 값을 가져옴 
            mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition);

            dir = clickPosition - mousePosition;
            Debug.Log("dir.x : " + dir);

           // tr.position = Vector3.Lerp(tr.position, tr.position + (Vector3.right * dir.x * (float)0.01), 

            tr.RotateAround(targetTr.position, Vector3.up, 20 * Time.deltaTime);

            // 휠을 누른채 오른쪽으로 드래그했을 때 
            if (dir.x < 0)
            {
                Debug.Log(">>>>");
                // 카메라가 targetTr을 중심으로 right로 회전
                tr.RotateAround(targetTr.position, Vector3.up, 2);
            }
            // 휠을 누른채 왼쪽으로 드래그 했을 때 
            else if(dir.x > 0)
            {
                Debug.Log("<<<<");
                //★ tr.RotateAround(targetTr.position, Vector3.up , -20 * Time.deltaTime); 은 동작 x 
                tr.RotateAround(targetTr.position, Vector3.up , -2);

            }
        }
    }

    void LateUpdate()
    {
        tr.LookAt(targetTr.position);
    }


    // Stage1_Manager에서의 흐름,SelectUnit 스크립트에서의 조작에 따라 targetTr의 값 변경 
    public void FocusingTarget(GameObject target)
    {
        targetTr = target.transform;
        //targetTr = GameObject.Find(target).transform;
        //if (GameObject.Find(target) == null) Debug.Log("타겟을 찾을 수 없습니다. 코딩에러 ");
    }

    public void FocusingTargetString(string target)
    {
        targetTr = GameObject.Find(target).transform;
    }

    public void MoveCamera(Vector3 position, Quaternion rotation)
    {
        this.transform.position = Vector3.MoveTowards(tr.position, position, Time.deltaTime * dampTrace);
        this.transform.rotation = Quaternion.Lerp(tr.rotation, rotation, Time.deltaTime * dampTrace);
    }

    public void DistanceChagne(float x, float y, float z)
    {
        basicX = x;
        basicY = y;
        basicZ = z;
    }


    /*
    public void FollowTargetString(string target)
    {
        followMode = true;
        StartCoroutine(FollowTarget(target));
    }

    IEnumerator FollowTarget(string target)
    {
        while(followMode)
        {
            targetTr = GameObject.Find(target).transform;
            //float distanceX = targetTr.position.x - tr.position.x;
            //float distanceZ = targetTr.position.z - tr.position.z;
            Vector3 followPosition = new Vector3(targetTr.position.x + 23.66f, tr.position.y, targetTr.position.z + 10.27f);


            tr.position = Vector3.MoveTowards(tr.position,  followPosition, Time.deltaTime * dampTrace);

            yield return new WaitForSeconds(0.01f);
        }
    }
    */

}
