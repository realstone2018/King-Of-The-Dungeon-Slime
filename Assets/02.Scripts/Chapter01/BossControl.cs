using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    //Player의 생명변수
    public int hp = 100;
    //Player의 생명 초깃값 
    private int initHp;
    //Player의 Health bar 이미지 
    public Image imgHpbar;

    // 보스애니메이션 실행을 위한 컴포넌트 변수 
    private Animator animator;
    public Transform bossStartTr;

    // 이펙트 변수들 
    public GameObject dust;

    // 사운드 변수들
    private AudioSource source = null;
    public AudioClip sweepAudio;
    public AudioClip smashAudio;

    // 딜리게이트 
    public delegate void BossDieHandler();
    public static event BossDieHandler OnBossDie;


    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        // 애니메이션을 위한 위치조정 시 초기화를 위한 초기값 
        bossStartTr = this.transform;

        StartCoroutine(Idle());
    }

    void OnTriggerEnter(Collider coll)
    {
        // 충돌한 Cliider가 몬스터이면 HP 차감 
        if (coll.gameObject.tag == "BULLET_CYAN" || coll.gameObject.tag == "BULLET_MAGENTA" || coll.gameObject.tag == "BULLET_YELLOW" || coll.gameObject.tag == "BULLET_RED" || coll.gameObject.tag == "BULLET_GREEN" || coll.gameObject.tag == "BULLET_BLUE" || coll.gameObject.tag == "BULLET_BLACK")
        {
            Debug.Log("Hit!!!");

            hp -= 6;
            //Image UI 항목의 fillAmount 속성을 조절해 생명 게이지 값 조절 
            imgHpbar.fillAmount = (float)hp / 100f;

            if (hp <= 0)
            {
                BossDie();
            }
        }
    }

    // Manager에서 호출하기 위한 public 함수들 
    public void Pattern1()
    {
        StartCoroutine(SweepPattern());
    }

    public void Pattern2()
    {
        StartCoroutine(SmashPattern());
    }

    public void Pattern3()
    {
        StartCoroutine(PunchPattern());
    }

    // 죽었을 경우 딜리게이트 이벤트 
    void BossDie()
    {
        StopAllCoroutines();
        OnBossDie();
        StartCoroutine(NextScene());
    }

    // 위아래 둥둥 떠있는 idle 상태 
    IEnumerator Idle()
    {
        while (true)
        {
            for (int i = 0; i < 8; i++) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 10, 0), 1);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 8; i++)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -20, 0), 1);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 한팔로 스테이지를 휩쓰는 패턴 
    IEnumerator SweepPattern()
    {
        Debug.Log("Pattern1");

        // 골렘 높이를 애니메이션에 맞춰서 스테이지를 쓸수있게 
        for (int i = 0; i < 65; i++)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(613, 442, -1044), 1);
            yield return new WaitForSeconds(0.03f);
        }

        animator.SetBool("Pattern1", true);

        // 골렘 애니메이션 작동죽일때 회전을 줘서 스테이지 전체를 타격하게

        yield return new WaitForSeconds(3.8f);
        Camera.main.GetComponent<ShakingCamera>().ShakeCamera(1.0f);

        source.PlayOneShot(sweepAudio, 0.5f);

        yield return new WaitForSeconds(0.5f);
        dust.SetActive(true);

        animator.SetBool("Pattern1", false);

        for (int i = 0; i < 65; i++)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, bossStartTr.position, 1);
            yield return new WaitForSeconds(0.05f);
        }

        dust.SetActive(false);
    }

    // 두팔로 스테이지를 내려찍는 패턴 
    IEnumerator SmashPattern()
    {
        Debug.Log("Pattern2");

        for (int i = 0; i < 65; i++)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(613, 500, -1044), 1);
            yield return new WaitForSeconds(0.03f);
        }

        animator.SetBool("Pattern2", true);
        yield return new WaitForSeconds(2.5f);
        Camera.main.GetComponent<ShakingCamera>().ShakeCamera(1.0f);
        source.PlayOneShot(smashAudio, 0.8f);
        dust.SetActive(true);

        animator.SetBool("Pattern2", false);

        for (int i = 0; i < 65; i++)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, bossStartTr.position, 1);
            yield return new WaitForSeconds(0.05f);
        }

        dust.SetActive(false);
    }

    // 전방 직선 공격 패턴  (애니메이션이 부자연스러워서 본게임에선 제외)
    IEnumerator PunchPattern()
    {
        Debug.Log("Pattern3");

        animator.SetBool("Pattern3", true);
        yield return null;

        animator.SetBool("Pattern3", false);
    }

    // 보스가 죽었을 경우 Chaptor02로 이동 
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Chaptor02");
    }
}
