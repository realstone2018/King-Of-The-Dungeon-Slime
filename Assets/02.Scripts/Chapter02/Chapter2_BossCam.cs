using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_BossCam : MonoBehaviour {
    public GameObject targetOb;
    public Transform targetTr;
    public Transform tr;

    public float dist = 20.0f;
    public float height = 15.0f;
    public float dampTrace = 20.0f;

    public int speed = 45;
    public float w = 0.0f;
    public float wheel = 1.2f;

    public Vector3 mousePosition;
    public Vector3 clickPosition;
    public Vector3 dir;

    public float basicX = 0; public float basicY = 0; public float basicZ = 0;



    public static Chapter2_BossCam instance = null;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tr = GetComponent<Transform>();
        targetTr = targetOb.transform;
        //tr.position = Vector3.Lerp(tr.position, targetTr.position - (Vector3.forward * 240) -
        //               (Vector3.up * 400) - (Vector3.right * -40), Time.deltaTime * dampTrace);
        StartCoroutine(BossStageDirect());
    }

    void Update()
    {
        // 휠 up&down에 따른 카메라 확대, 축소 
        w = Input.GetAxis("Mouse ScrollWheel");
        // wheel의값이 0.8 ~ 1.8 외의 값을 가질수 없게 한다.
        wheel = Mathf.Clamp(wheel, 0.8f, 1.8f);

        // 휠을 아래로 굴릴때 -값,  위로 굴리면 +값 
        if (w < 0)
        {
            //★ Translate는 기본적으로 local좌표값으로 이동, 그래서 back만해도 xyz값이 변경되었던것
            tr.Translate(Vector3.back * speed * Time.deltaTime);

            basicX = targetTr.position.x - tr.transform.position.x;
            basicY = targetTr.position.y - tr.transform.position.y;
            basicZ = targetTr.position.z - tr.transform.position.z;
        }
        else if (w > 0)
        {
            tr.Translate(Vector3.back * (-speed) * Time.deltaTime);

            basicX = targetTr.position.x - tr.transform.position.x;
            basicY = targetTr.position.y - tr.transform.position.y;
            basicZ = targetTr.position.z - tr.transform.position.z;
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
                tr.RotateAround(targetTr.position, Vector3.up, 2.0f);

                basicX = targetTr.position.x - tr.transform.position.x;
                basicY = targetTr.position.y - tr.transform.position.y;
                basicZ = targetTr.position.z - tr.transform.position.z;
            }
            // 휠을 누른채 왼쪽으로 드래그 했을 때 
            else if (dir.x > 0)
            {
                Debug.Log("<<<<");
                //★ tr.RotateAround(targetTr.position, Vector3.up , -20 * Time.deltaTime); 은 동작 x 
                tr.RotateAround(targetTr.position, Vector3.up, -2.0f);

                basicX = targetTr.position.x - tr.transform.position.x;
                basicY = targetTr.position.y - tr.transform.position.y;
                basicZ = targetTr.position.z - tr.transform.position.z;
            }
        }
    }

    void LateUpdate()
    {
        //tr.LookAt(targetTr.position);
    }


    IEnumerator BossStageDirect()
    {
        Quaternion destinationRot = Quaternion.Euler(25, 90, 0);
        Vector3 destination = new Vector3(-61.8f, 22.8f, 4f);

        while (tr.position != destination)
        {
            tr.position = Vector3.MoveTowards(tr.position, destination, 0.3f);
            tr.rotation = Quaternion.RotateTowards(tr.rotation, destinationRot, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.0f);
        // 타겟 추적 코루틴 시작 
        StartCoroutine(FollowTarget(targetOb));

    }

    public void FocusingTarget(GameObject target)
    {
        targetTr = target.transform;
        //targetTr = GameObject.Find(target).transform;
        //if (GameObject.Find(target) == null) Debug.Log("타겟을 찾을 수 없습니다. 코딩에러 ");
    }

    IEnumerator FollowTarget(GameObject target)
    {
        basicX = target.transform.position.x - tr.transform.position.x;
        basicY = target.transform.position.y - tr.transform.position.y;
        basicZ = target.transform.position.z - tr.transform.position.z;

        // 나중에 게임오버가 아닐때로 조건 바꿔주기 
        while (true)
        {


            Vector3 followPosition = new Vector3(targetTr.position.x - basicX, targetTr.position.y - basicY, targetTr.position.z - basicZ);
            tr.position = Vector3.MoveTowards(tr.position, followPosition, Time.deltaTime * dampTrace);
            //tr.position = followPosition;
            yield return new WaitForSeconds(0.01f);

            //tr.LookAt(targetTr.position);
        }
    }
}
