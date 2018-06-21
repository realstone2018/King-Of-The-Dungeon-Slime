using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour {

    public GameObject slime_C;
    public GameObject slime_M;
    public GameObject slime_Y;
    public GameObject slime_R;
    public GameObject slime_G;
    public GameObject slime_B;
    public GameObject slime_Black;

    public Vector3 position_c;
    public Vector3 position_y;
    public Vector3 crossPosition;


    //임펙트 오브젝트 프리팹 
    public GameObject effectPre;
    //프리팹으로 생성한 오브젝트를 담을 변수 
    public GameObject combineEffect;
    
    // 합체상태,  합체상태에선 이동상태가 false이 되어 이동중이던 작업을 취소시킨다.
    public bool combineState = false;

    // z이후 눌린키 
    public bool ready_C = false;
    public bool ready_M = false;
    public bool ready_Y = false;

    // 합체도중에도 input되는 경우를 막기위해 
    public bool inputWait = true;

    // 공중합체 
    public bool jumpping = false;

    public bool effectCall = true;
    public bool effectEnd = false;

    public bool redCall = false;
    public bool greenCall = false;
    public bool blueCall = false;
    public bool blackCall = false;

    //합체 쿨타임 
    public int coolCount;
    public bool coolDown = true;

    public CombineDisc combineDisc;

    // SelectUnit의 Awake에서 할당되고 Active(false)는 start에서 할당되므로  SelectUnit값을 가져오기위해 Start에서 할당 
    void Start () {
        slime_C = SelectUnit.instance.slime_C;
        slime_M = SelectUnit.instance.slime_M;
        slime_Y = SelectUnit.instance.slime_Y;
        slime_R = SelectUnit.instance.slime_R;
        slime_G = SelectUnit.instance.slime_G;
        slime_B = SelectUnit.instance.slime_B;
        slime_Black = SelectUnit.instance.slime_Black;

        /*
        slime_C = GameObject.Find("Slime_Cyan");
        slime_M = GameObject.Find("Slime_Magenta");
        slime_Y = GameObject.Find("Slime_Yellow");
        slime_R = GameObject.Find("Slime_Red");
        slime_G = GameObject.Find("Slime_Green");
        slime_B = GameObject.Find("Slime_Blue");
        slime_Black = GameObject.Find("Slime_Black");
        */

        StartCoroutine(CheckMixSlimeAct());
    }

    void Update () {

        if (Input.GetButtonDown("Z"))
        {
            if (coolDown)
            {
                combineState = true;
                coolDown = false;
                StartCoroutine(CoolDownCounting());
            }
            else
            {
                Debug.Log("아직 합체 할 수 없습니다");
                // 아직 합체할수 없습니다 텍스트출력 
                // filAmount로  쿨타임 출력 가능 할듯

            }
        }
        // Update에서 호출해야 슬라임들이 계속움직이는 문제 
        if(combineState)
        {
            StartCoroutine(SelectCombine());
            
        }
    }

    IEnumerator SelectCombine()
    {
        // 합체도중에도 input되는 경우를 막기위해 
        if (inputWait)
        {
            if (Input.GetButtonDown("1"))
            {
                Debug.Log("Ready Cyan");
                combineDisc.DiscBrighten("Cyan");
                ready_C = true;
            }
            if (Input.GetButtonDown("2"))
            {
                Debug.Log("Ready Magenta");
                combineDisc.DiscBrighten("Magenta");
                ready_M = true;
            }
            if (Input.GetButtonDown("3"))
            {
                Debug.Log("Ready Yellow");
                combineDisc.DiscBrighten("Yellow");
                ready_Y = true;
            }

            // 입력 대기 시간 
            yield return new WaitForSeconds(1.5f);
        }

        // 1, 2, 3 번이 눌렸을때 : Black
        if (ready_C && ready_M && ready_Y)
        {
            //SlimeCross(slime_C, slime_M, slime_Y);
            blackCall = true;
        }
        //1번과 2번이 눌렸을 때 : Cyan + Magenta = Blue
        else if (ready_C && ready_M && !ready_Y)
        {
            SlimeCross(slime_C, slime_M);
            blueCall = true;
        }
        //2번과 3번이 눌렸을 때 : Magenta + Yellow = Red
        else if (!ready_C && ready_M && ready_Y)
        {
            SlimeCross(slime_M, slime_Y);
            redCall = true;
        }
        //3번과 1번이 눌렸을 때 : Yellow + Cyan = Green
        else if (ready_C && !ready_M && ready_Y)
        {
            SlimeCross(slime_Y, slime_C);
            greenCall = true;
        }


    }

    // 2마리만 합칠때  
    void SlimeCross(GameObject slime1, GameObject slime2)
    {
        position_c = slime1.transform.position;
        position_y = slime2.transform.position;
        // 두 슬라임간의 거리 계산
        float dir = Vector3.Distance(position_c, position_y);
        // 두 슬라임의 중앙 Vector3값 계산 
        crossPosition = (position_c + position_y) / 2.0f;

        //Debug.Log("CrossPositon : " + crossPosition);
        //Debug.Log("Distance : " + dir);

        // 선택된 두 슬라임의 MoveObject(CrossPosition) 함수 호출, crossPosition으로 이동 
        slime1.GetComponent<SlimeControl>().MoveObject(crossPosition);
        slime2.GetComponent<SlimeControl>().MoveObject(crossPosition);

        // 두 슬라임간의 거리가 10이하고 jumpping중이 아닐때 지점에서 점프,
        if (3.0 < dir && dir < 10.0 && !jumpping)
        {
            slime1.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f);
            slime2.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f);
            jumpping = true;

        }
        // 점프 중이고 거리가 1이하면 공중 합체 
        else if (dir <= 4.0f)
        {
            //  두 슬라임을 없애고 
            slime1.SetActive(false);
            slime2.SetActive(false);
            // 다시 활성화 때 튀어오르는것을 막기위해 초기화
            slime1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            slime1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            slime2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            slime2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // 임펙트가 여러번 호출되는것을 막기위해 
            if (effectCall)
            {
                effectCall = false;
                StartCoroutine(CombineEffect());
            }

            // 이펙트 코루틴이 종료되면 혼합 슬라임을 CrossPositon위치로 이동 후 SetActive 

            if (effectEnd)
            {
                if (redCall)
                {
                    slime_R.transform.position = crossPosition;
                    slime_R.SetActive(true);
                    //slime_R.GetComponent<SlimeControl>().MoveObject(crossPosition);
                }
                else if (greenCall)
                {
                    slime_G.transform.position = crossPosition;
                    slime_G.SetActive(true);
					//slime_G.GetComponent<SlimeControl>().MoveObject(crossPosition);
					if (FixCam.instance.targetTr.name == "Slime_Cyan") 
						FixCam.instance.FocusingTargetString("Slime_Green");
                }
                else if (blueCall)
                {
					Debug.Log(FixCam.instance.targetTr.name);
                    slime_B.transform.position = crossPosition;
                    slime_B.SetActive(true);
					//slime_B.GetComponent<SlimeControl>().MoveObject(crossPosition);
					if (FixCam.instance.targetTr.name == "Slime_Cyan")
						FixCam.instance.FocusingTargetString("Slime_Blue");
				}
                else if (blackCall)
                {

                    slime_Black.transform.position = crossPosition;
                    slime_Black.SetActive(true);
                    slime_Black.GetComponent<SlimeControl>().MoveObject(crossPosition);
                }

                // 초기화 
                combineState = false;
                jumpping = false;
                effectCall = true;
                effectEnd = false;
                inputWait = true;
                ready_C = false;
                ready_M = false;
                ready_Y = false;

            }
            // 합체가 제대로 이루어지지 않았을때 

            combineDisc.ReSetCombineDisc();
        }
    }


    // 3마리가 전부 합쳐질 때 (오버로딩)   <- 아직 손 안됨  복붙만해놓음 
    public void SlimeCross(GameObject slime_A, GameObject slime_B, GameObject slime_C)
    {
        /*
        position_c = slime_A.transform.position;
        position_y = slime_B.transform.position;
        float dir = Vector3.Distance(position_c, position_y);
        crossPosition = (position_c + position_y) / 2.0f;

        Debug.Log("CrossPositon : " + crossPosition);
        Debug.Log("Distance : " + dir);

        // 선택된 두 슬라임(여기선 c, m)의 MoveObject(CrossPosition) 함수 호출 필요 
        slime_A.GetComponent<SlimeControl>().MoveObject(crossPosition);
        slime_B.GetComponent<SlimeControl>().MoveObject(crossPosition);

        // 거리가 10인 지점에서 점프,
        if (1.0 < dir && dir < 10.0 && !jumpping)
        {
            slime_A.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f);
            slime_B.GetComponent<Rigidbody>().AddForce(transform.up * 1000.0f);
            jumpping = true;

        }
        // 점프 후 공중 합체 
        else if (dir < 1.0f)
        {
            //  두 슬라임을 없애고 
            slime_A.SetActive(false);
            slime_B.SetActive(false);

            // 임펙트 호출 
            if (effectCall)
            {
                StartCoroutine(this.CombineEffect());
                effectCall = false;
            }

            // 혼합 슬라임 CrossPositon위치로 이동 후 SetActive 

            if (effectEnd)
            {
                slime_G.transform.position = crossPosition;
                slime_G.SetActive(true);

                slime_G.GetComponent<SlimeControl>().MoveObject(crossPosition);


                // 초기화 
                jumpping = false;
                combineState = false;
                effectCall = true;
                greenCall = false;
            }
        }
        */
    }
 
    IEnumerator CombineEffect()
    {
        combineEffect = (GameObject)Instantiate(effectPre, crossPosition, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);

        Destroy(combineEffect);

        effectEnd = true;
    }

    IEnumerator CoolDownCounting()
    {
        coolCount = 20;
        while (coolCount > 0)
        {
            coolCount--;
            Debug.Log("Combine CoolTime : " + coolCount);
            yield return new WaitForSeconds(1.0f);
        }
        coolDown = true;
    }
    
    IEnumerator CheckMixSlimeAct()

    {            
        // gameObject.Find함수들이 찾을 수 있게 혼합슬라임들 초기상태는 활성화상태이므로 안전빵 30초정도 기다리기 
        yield return new WaitForSeconds(10.0f);

        while (true)
        {
            if (slime_R.activeSelf)
            {
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    // 5초를 한번에 기다리지 않는 이유 : 변수를 1초마다 -- 하여  남은 유지시간 UI 보여주기 위해 
                }
                // Magenta Yellow 슬라임들의 위치를 Red슬라임위치로 옮긴후 활성화시키기 
                Vector3 divisionPoint = slime_R.transform.position + new Vector3(0, 5f, 0);
                slime_M.transform.position = divisionPoint + new Vector3(2.0f, 0, 0);
                slime_Y.transform.position = divisionPoint + new Vector3(-2.0f, 0, 0); ;

                // Red 슬라임의 HP은 유지시켜야함, 나머지는 에러시 비활성화 후 초기화 시켜주기 
                slime_R.SetActive(false);

                slime_M.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                slime_Y.SetActive(true);

                redCall = false;
            }
            else if (slime_G.activeSelf)
            {
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    // 5초를 한번에 기다리지 않는 이유 : 변수를 1초마다 -- 하여  남은 유지시간 UI 보여주기 위해 
                }
                Vector3 divisionPoint = slime_G.transform.position + new Vector3(0, 5f, 0);
                slime_C.transform.position = divisionPoint + new Vector3(2.0f, 0, 0);
                slime_Y.transform.position = divisionPoint + new Vector3(-2.0f, 0, 0); ;

                slime_G.SetActive(false);

                slime_C.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                slime_Y.SetActive(true);


                greenCall = false;

				if (FixCam.instance.targetTr.name == "Slime_Green")
					FixCam.instance.FocusingTargetString("Slime_Cyan");
            }
            else if (slime_B.activeSelf)
            {
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    // 5초를 한번에 기다리지 않는 이유 : 변수를 1초마다 -- 하여  남은 유지시간 UI 보여주기 위해 
                }
                Vector3 divisionPoint = slime_B.transform.position + new Vector3(0, 5f, 0);
                slime_C.transform.position = divisionPoint + new Vector3(2.0f, 0, 0);
                slime_M.transform.position = divisionPoint + new Vector3(-2.0f, 0, 0); 

                slime_B.SetActive(false);

                slime_C.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                slime_M.SetActive(true);


                blueCall = false;;

				if (FixCam.instance.targetTr.name == "Slime_Blue")
					FixCam.instance.FocusingTargetString("Slime_Cyan");
			}
            else if (slime_Black.activeSelf)
            {
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    // 5초를 한번에 기다리지 않는 이유 : 변수를 1초마다 -- 하여  남은 유지시간 UI 보여주기 위해 
                }
                Vector3 divisionPoint = slime_Black.transform.position + new Vector3(0, 5f, 0);
                slime_C.transform.position = divisionPoint + new Vector3(2.0f, 0, 0);
                slime_M.transform.position = divisionPoint + new Vector3(-2.0f, 0, 0);
                slime_Y.transform.position = divisionPoint + new Vector3(-2.0f, 0, 2.0f);

                slime_Black.SetActive(false);

                slime_C.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                slime_M.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                slime_Y.SetActive(true);

                blackCall = false;
            }
            // 1초 간격으로 혼합 체크 
            yield return new WaitForSeconds(1.0f);
        }
    }
}