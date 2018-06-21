 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grave : MonoBehaviour { 
    public GameObject ghost;
    // 슬라임 탄환을 2번 맞을 시 제거 

    // 오브젝트 UI를 이용해 위에 HP표시 
    public int hp = 3;
    //Player의 Health bar 이미지 
    public Image imgHpbar;


    // 7초에 한번씩 유령1마리 소환
    public GameObject smoke;

    public bool appearFinish = false;


    public AudioSource source = null;
    public AudioClip upClip;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Chapter2_Manager.instance.graveCnt++;
    }

    // 초기화는 탄을 맞은 직후,  5초후 비활성화시 모든 코루틴을 멈추게 
    void OnDisable()
    {
        Chapter2_Manager.instance.graveCnt--;
    }

    IEnumerator CreateGhost()
    {
        while (true)
        {
            if (!ghost.activeSelf)
            {
                Debug.Log("CreateGhost() ");
                ghost.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                ghost.SetActive(true);
                // 네비게이션은 active되고나서 Ghost 스크립트에서  활성화하기
            }
            yield return new WaitForSeconds(6.0f);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (appearFinish)
        {
            if (coll.gameObject.tag == "BULLET_CYAN" || coll.gameObject.tag == "BULLET_MAGENTA" || coll.gameObject.tag == "BULLET_YELLOW" || coll.gameObject.tag == "BULLET_RED" || coll.gameObject.tag == "BULLET_GREEN" || coll.gameObject.tag == "BULLET_BLUE" || coll.gameObject.tag == "BULLET_BLACK")
            {
                Destroy(coll.gameObject);

                hp--;
                //Image UI 항목의 fillAmount 속성을 조절해 생명 게이지 값 조절 
                imgHpbar.fillAmount = (float)hp / 3f;
            }

            if (hp <= 0)
            {
                DestroyGrave();
            }
            // 네이버게이션 비활성화 , 네임스페이스 추가한 후 작성하기 
            //초기화 
        }
    }


    public void Appear()
    {
        this.transform.position = new Vector3(transform.position.x, -22f, transform.position.z);
        smoke.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        this.gameObject.SetActive(true);
        smoke.SetActive(true);

        StartCoroutine(UpPositionY());
    }

    IEnumerator UpPositionY()
    {
        source.PlayOneShot(upClip, 0.1f);
        //무덤 y값 - 5로 주고   y값 점점  올리는 코루틴 작성
        //Debug.Log("UpPositionY()");

        while (transform.position.y <= -1)
        {
            //Debug.Log("UpPositionY().While()");
            this.transform.Translate(Vector3.up * 0.12f);
            yield return new WaitForSeconds(0.01f);
        }
        smoke.SetActive(false);
        appearFinish = true;
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(CreateGhost());

        yield return null;
    }

    void DestroyGrave()
    {
        this.gameObject.SetActive(false);
        StopAllCoroutines();

        //초기화
        hp = 3;
        imgHpbar.fillAmount = 1;
    }
}