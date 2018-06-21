using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//참조스크립트 (JumppingSound jumpScript(변수로), CombineManager(GameObject로 가져와서 GetComponent))  <- 오브젝트로가져와서 GetComponent하니 한눈에 파악안됨 주의 
public class SlimeControl : MonoBehaviour {
    public GameObject combineManager;
    // 슬라임 자체에 오디오 클립 넣을 시 오류발생, 따로 스크립트로 작성 
    public JumppingSound jumpScript;

    public float moveSpeed = 22.0f;
    public float turnSpeed = 1000f;
    public bool moveControl = true;
    int clickLayer = 14;

    // 이동하는 방향을 바라보는 회전값이 들어갈 Quarternion
    public Quaternion frameRot;

    // 이동중일때만 true;  
    bool isMoveState = false;
    // 마우스로 클릭한 곳의 위치정정보를 담을 Vector3
    Vector3 hitPosition;

    // 사운드 재생을 위한 변수 
    private AudioSource source = null;

    public int monsterDamage = 3;
    public int aoeDamage = 3;

    public bool invincibility = false;
    public int slimeColorId = 0;
    public int aoeColorId = 0;

    void Start()
    {
        // Combine 컴포넌트에서 변수값을 가져오기 위해 
        combineManager = GameObject.Find ("Controller/CombineManager");
        source = GetComponent<AudioSource>();

        StartCoroutine(JumpSoundStart());
    }

    void Update()
    {
        // Combine스크립트의 conbineState값을 가져옴,  합체 시 이동중이던걸 멈추고 합체하려 달려가기위해 
         bool combineState = combineManager.GetComponent<Combine>().combineState;

        //마우스 왼쪽 버튼이 눌리면
        if (Input.GetMouseButtonUp(1) && moveControl)
        {
            MouseEvent();
            //ManaualGravity();
        }
        // 이동상태고 합체상태가 아닐때  => 이동중인데 합체상태가되면 MoveObject함수 호출 중지된다.
        if (isMoveState && !combineState)
        {
            MoveObject(hitPosition);
        }
    }

    void MouseEvent()
    { 
        //Debug.Log("MouseClick");

        // 카메라로부터(시작점) + 커서위치까지(방향)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //충돌 오브젝트를 저장할 구조체 변수 
        RaycastHit hitInfo;

        // Raycast(시작점+방향, 충돌오브젝트 저장변수, 최대거리), 충돌시 true 반환 
        if (Physics.Raycast(ray, out hitInfo, 100000f))
        {
            // 충돌정보를 저장한 구조체에서 충돌한 오브젝트의 layer값을 l에 저장 
            int l = hitInfo.transform.gameObject.layer;

            // layer값이 클릭 레이어면 (이동할수 있는 레이어) 
            if (l == clickLayer)
            {
                //Debug.Log(" hit object : " + hitInfo.collider.name);

                hitPosition = hitInfo.point;
                isMoveState = true;
            }
        }
    }

    public void MoveObject(Vector3 destination)
    {
        //거리 = 히트지점 - 현재 위치 
        Vector3 dir = destination - transform.position;
        //거리 벡터에서 y값 제거
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);

        // 현재위치를 클릭한게 아니라면 
        if (dirXZ != Vector3.zero)
        {
            // dirXZ값을 가지고 rotation 생성 , ?????
            Quaternion targetRot = Quaternion.LookRotation(dirXZ);
            //현재rotation에서 target을 바라보도록 일정 속도로 회전 
            frameRot = Quaternion.RotateTowards(transform.rotation,
                                                           targetRot,
                                                           turnSpeed * Time.deltaTime);
            
            transform.rotation = frameRot;
        }


        //타겟의위치 = 현재위치 + 거리벡터, y값을 제외한 거리벡터로 타겟위치를 계산
        Vector3 targetPos = transform.position + dirXZ;

        //현재위치 -> 타겟위치로  일정 속도로 움직이는 함수호출, 함수의 반환값은 현재 위치이다.
        Vector3 framePos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        // 중력 -9.81  벡터
        Vector3 moveDir = (framePos - transform.position) + Physics.gravity;

        //대체 필요
        //cc.Move(moveDir);
        // 중력값을 Move로 줄땐 상관없으나 position에 대입하니 땅으로 꺼지는현상, Translate 함수면 괜찮을듯
        this.transform.position = framePos;

        //MoveTowards가 반환하는 값 알아보기 
        if (framePos == targetPos)
        {
            isMoveState = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("MONSTER")) {
            SlimeHp.instance.DecreaseHp(monsterDamage);
            return;
        }
        
        // 충돌 AOE 종류 분류 
        if (coll.GetComponent<CircleAOE>() != null)
            aoeColorId = coll.GetComponent<CircleAOE>().aoeColorId;
        else if (coll.GetComponent<DiamondAOE>() != null)
            aoeColorId = coll.GetComponent<DiamondAOE>().aoeColorId;
        else if (coll.GetComponent<ArrowAOE>() != null)
            aoeColorId = coll.GetComponent<ArrowAOE>().aoeColorId;
        else if (coll.GetComponent<SquareAOE>() != null)
            aoeColorId = coll.GetComponent<SquareAOE>().aoeColorId;
        else if (coll.GetComponent<StripedAOE>() != null)
            aoeColorId = coll.GetComponent<StripedAOE>().aoeColorId;
        else
            return;
        
        ColorCheck(aoeColorId);
    }

    void ColorCheck(int aoeColorId)
    {
        if (slimeColorId == aoeColorId)
            // 장판 중간에 구멍이 뚫려있는곳으로 피해도 Collider Box는 구멍이 아니므로 2초간 무적상태로 
            StartCoroutine(InvincibilityActivity());
        else
            // Slme Cyan은 cyan색이 아닌 aoe장판과 충돌 시 1초후 무적상태가 아니라면 데미지를 입는다.   
            StartCoroutine(AoeAttackDecision());
    }

	// Chapter01 Openning 시 점프소리가 나므로 3.5초 후 점프사운드 시작
	IEnumerator JumpSoundStart()
    {
        yield return new WaitForSeconds(3.5f);
        jumpScript.enabled = true;
    }

    IEnumerator AoeAttackDecision()
    {
        yield return new WaitForSeconds(1.0f);

		if (!invincibility)
		{
			SlimeHp.instance.DecreaseHp(aoeDamage);
		}
		else Debug.Log("AoeDamage is enEffect by Invincibility");
    }

    IEnumerator InvincibilityActivity()
    {

		invincibility = true;

        yield return new WaitForSeconds(2.0f);

        invincibility = false;
	}
}
