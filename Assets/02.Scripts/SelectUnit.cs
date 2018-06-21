
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    // 각 슬라임 오브젝트 변수
    public GameObject slime_C;
    public GameObject slime_M;
    public GameObject slime_Y;
    public GameObject slime_R;
    public GameObject slime_G;
    public GameObject slime_B;
    public GameObject slime_Black;

    // 각 슬라임 오브젝트들의 컨트롤 스크립트 
    public SlimeControl mousectrl_C;
    public SlimeControl mousectrl_M;
    public SlimeControl mousectrl_Y;
    public SlimeControl mousectrl_R;
    public SlimeControl mousectrl_G;
    public SlimeControl mousectrl_B;
    public SlimeControl mousectrl_Black;
   
    // 각 슬라임 오브젝트들의 fire 컨트롤 스크립트
    public FireCtrl fireCtrl_C;
    public FireCtrl fireCtrl_M;
    public FireCtrl fireCtrl_Y;
    public FireCtrl fireCtrl_R;
    public FireCtrl fireCtrl_G;
    public FireCtrl fireCtrl_B;
    public FireCtrl fireCtrl_Black;

    // 각 슬라임들의 조종 표식 
    public GameObject con_C;
    public GameObject con_M;
    public GameObject con_Y;
    public GameObject con_R;
    public GameObject con_G;
    public GameObject con_B;
    public GameObject con_Black;

    // 여기저기서 슬라임객체에 접근하므로 여기서만 Find하고 나머지는 여기값을 받아가게  싱글턴패턴 사용 
    public static SelectUnit instance = null;

    // 같은 키를 두번 누를 경우 FixCam 스크립트의 타겟이 바뀐다. ( ` = 0 )  
    public int oldInput = 0;
    // FixCam 스크립트의 변수를 변경하기위해 FixCam을 포함하고있는 오브젝트 저장 변수
    public GameObject cameraObject;
    // 포커싱을 풀었을 때의 basic 카메라 타겟 
    public GameObject center;
    // 포커싱 상태에서 다른 오브젝트는 화면에 안보이므로 조작 불편, ex) 1 1  2 했을때   
    // 11 2 했을 땐  2번 이 한번 눌려도 2번 포커싱상태 되게 하기 
    // focursingState가 true일때 ` 1 2 3 번을 누르면 포커싱, 4번누르면 전체화면으로 

    public bool focursingState;

    void Awake()
    {
        instance = this;

        slime_C = GameObject.Find("Slime_Cyan");
        slime_M = GameObject.Find("Slime_Magenta");
        slime_Y = GameObject.Find("Slime_Yellow");
        // 슬라임_그린 할당 후 바로 비활성화,  합체시에 활성화 
        slime_R = GameObject.Find("Slime_Red");
        slime_G = GameObject.Find("Slime_Green");
        slime_B = GameObject.Find("Slime_Blue");
        slime_Black = GameObject.Find("Slime_Black");

        mousectrl_C = slime_C.GetComponent<SlimeControl>();
        mousectrl_M = slime_M.GetComponent<SlimeControl>();
        mousectrl_Y = slime_Y.GetComponent<SlimeControl>();
        mousectrl_R = slime_R.GetComponent<SlimeControl>();
        mousectrl_G = slime_G.GetComponent<SlimeControl>();
        mousectrl_B = slime_B.GetComponent<SlimeControl>();
        mousectrl_Black = slime_Black.GetComponent<SlimeControl>();

        // 자식 오브젝트를 가져오는 방법 2가지 
        con_C = GameObject.Find("Slimes/Slime_Cyan/CyanSlimeModel/Con");
        con_M = GameObject.Find("Slimes/Slime_Magenta/MagentaSlimeModel/Con");
        con_Y = GameObject.Find("Slimes/Slime_Yellow/YellowSlimeModel/Con");
        con_R = GameObject.Find("Slimes/Slime_Red/RedSlimeModel/Con");
        con_G = GameObject.Find("Slimes/Slime_Green/GreenSlimeModel/Con");
        con_B = GameObject.Find("Slimes/Slime_Blue/BlueSlimeModel/Con");
        con_Black = GameObject.Find("Slimes/Slime_Black/Con");

        // 자식의 컴포넌트를 가져올 땐 as FrieCtrl 을 붙여줘야 한다. ,  <- 이렇게 찾는게 더 확장성이좋다. 위방법보단
        fireCtrl_C = slime_C.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_M = slime_M.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_Y = slime_Y.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_R = slime_R.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_G = slime_G.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_B = slime_B.GetComponentInChildren<FireCtrl>() as FireCtrl;
        fireCtrl_Black = slime_Black.GetComponentInChildren<FireCtrl>() as FireCtrl;
    }

    void Start()
    {
        // 혼합 슬라임들은 Combine 스크립트를 통해서만 활성화 
        slime_R.SetActive(false);
        slime_G.SetActive(false);
        slime_B.SetActive(false);
        slime_Black.SetActive(false);
    }

    void Update()
    {
        Select();
    }

    void Select()
    {
        // Cyan 개별조작
        if (Input.GetButtonDown("1"))
        {
            //Debug.Log("1 button ");
            mousectrl_C.moveControl = true;
            mousectrl_M.moveControl = false;
            mousectrl_Y.moveControl = false;
            mousectrl_R.moveControl = false;

            con_C.SetActive(true);
            con_M.SetActive(false);
            con_Y.SetActive(false);
            con_R.SetActive(false);

            fireCtrl_C.fireControl = true;
            fireCtrl_M.fireControl = false;
            fireCtrl_Y.fireControl = false;
            fireCtrl_R.fireControl = false;
            /*
            // 1번을 누른상태(Cyan조작 중) 1번을 한번 더 누르면 카메라 포커싱 
            if (oldInput == 1)
            {
                Debug.Log(" Focursing 1");
                cameraObject.GetComponent<FixCam>().FocusingTarget(slime_C);
            }
            else
            {
                oldInput = 1;
            }
            */
        }
        // Magenta 개별 조각
        else if (Input.GetButtonDown("2"))
        {
            //Debug.Log("2 button ");
            mousectrl_C.moveControl = false;
            mousectrl_M.moveControl = true;
            mousectrl_Y.moveControl = false;
            mousectrl_G.moveControl = false;

            con_C.SetActive(false);
            con_M.SetActive(true);
            con_Y.SetActive(false);
            con_G.SetActive(false);

            fireCtrl_C.fireControl = false;
            fireCtrl_M.fireControl = true;
            fireCtrl_Y.fireControl = false;
            fireCtrl_G.fireControl = false;
            /*
            if (oldInput == 2)
            {
                cameraObject.GetComponent<FixCam>().FocusingTarget(slime_M);
            }
            else
            {
                oldInput = 2;
            }
            */
        }
        // Yellow 개별조작
        else if (Input.GetButtonDown("3"))
        {
            mousectrl_C.moveControl = false;
            mousectrl_M.moveControl = false;
            mousectrl_Y.moveControl = true;
            mousectrl_B.moveControl = false;

            con_C.SetActive(false);
            con_M.SetActive(false);
            con_Y.SetActive(true);
            con_B.SetActive(false);

            fireCtrl_C.fireControl = false;
            fireCtrl_M.fireControl = false;
            fireCtrl_Y.fireControl = true;
            fireCtrl_B.fireControl = false;
            //Slime_Cyan.GetComponent<PlayerCtrl>().enabled = false;
            //Slime_Magenta.GetComponent<PlayerCtrl>().enabled = false;
            //Slime_Yellow.GetComponent<PlayerCtrl>().enabled = true;
            /*
            if (oldInput == 3)
            {
                cameraObject.GetComponent<FixCam>().FocusingTarget(slime_Y);
            }
            else
            {
                oldInput = 3;
            }
            */
        }
        // 전체 조작 
        else if (Input.GetButtonDown("4"))
        {
            mousectrl_C.moveControl = true;
            mousectrl_M.moveControl = true; ;
            mousectrl_Y.moveControl = true;
            mousectrl_R.moveControl = true;
            mousectrl_G.moveControl = true;
            mousectrl_B.moveControl = true;

            con_C.SetActive(true);
            con_M.SetActive(true);
            con_Y.SetActive(true);
            con_R.SetActive(true);
            con_G.SetActive(true);
            con_B.SetActive(true);

            fireCtrl_C.fireControl = false;
            fireCtrl_M.fireControl = false;
            fireCtrl_Y.fireControl = false;
            fireCtrl_R.fireControl = true;
            fireCtrl_G.fireControl = true;
            fireCtrl_B.fireControl = true;

            // 전체 조작 시에는 무적건 포커싱이 풀리게 
            //cameraObject.GetComponent<FixCam>().FocusingTarget(center);
            //oldInput = 4;
        }
        else if (Input.GetButtonDown("`"))
        {
            // 블랙 슬라임이활성화 되어있지않은 경우   RGB는 동시에 올 수 없으니 한번에 처리하기 
            if (!slime_Black.activeSelf)
            {
                mousectrl_C.moveControl = false;
                mousectrl_M.moveControl = false;
                mousectrl_Y.moveControl = false;
                mousectrl_R.moveControl = true;
                mousectrl_G.moveControl = true;
                mousectrl_B.moveControl = true;

                con_C.SetActive(false);
                con_M.SetActive(false);
                con_Y.SetActive(false);
                con_R.SetActive(true);
                con_G.SetActive(true);
                con_B.SetActive(true);

                fireCtrl_C.fireControl = false;
                fireCtrl_M.fireControl = false;
                fireCtrl_Y.fireControl = false;
                fireCtrl_R.fireControl = true;
                fireCtrl_G.fireControl = true;
                fireCtrl_B.fireControl = true;
                /* 
                if (oldInput == 0)
                {
                    //RGB를 동시에 다루므로 활성화되어있는 오브젝트를 넘길필요 있음 
                    if (slime_R.activeSelf)
                    {
                        cameraObject.GetComponent<FixCam>().FocusingTarget(slime_R);
                    }
                    else if (slime_G.activeSelf)
                    {
                        cameraObject.GetComponent<FixCam>().FocusingTarget(slime_G);
                    }
                    else
                    {
                        cameraObject.GetComponent<FixCam>().FocusingTarget(slime_B);
                    }
                }
                else
                {
                    oldInput = 0;
                }
                */
            }
            // 블랙이 활성화 되어있는 경우 
            else
            {
                mousectrl_C.moveControl = false;
                mousectrl_M.moveControl = false;
                mousectrl_Y.moveControl = false;
                mousectrl_Black.moveControl = true;
                
                con_C.SetActive(false);
                con_M.SetActive(false);
                con_Y.SetActive(false);
                con_Black.SetActive(true);

                fireCtrl_C.fireControl = false;
                fireCtrl_M.fireControl = false;
                fireCtrl_Y.fireControl = false;
                fireCtrl_Black.fireControl = true;
                /*
                if (oldInput == 0)
                {
                    cameraObject.GetComponent<FixCam>().FocusingTarget(slime_Black);

                }
                else
                {
                    oldInput = 0;
                }
                */
            }
        }
    }
}