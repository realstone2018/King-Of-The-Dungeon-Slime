using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 참조스크립트(BossControl(변수로), CreateMonster(instance))
// 보스 패턴을 팔에 AttackTrigger을 두고 구현했기때문에 aoe를 집어넣지않음 
// aoe 시스템으로 대체할 경우 팔의 Trigger을 지우고  범위표시, 범위크기만큼 AttackTrigger 생성하기로 구현가능 
public class BossStage_Manager : MonoBehaviour {
    public GameObject boss;
    public GameObject gem;
    public GameObject bossStage_Center;
    public GameObject dropStone1;
    public GameObject dropStone2;

    // BossControl 참조 변수 
    public BossControl bossControl;

    // 사운드 변수들 
    private AudioSource source = null;
    public AudioClip golemRoar;
    public AudioClip bossStageBGM;
    // 이펙트 변수들 
    public GameObject dieEffect;

    //카메라 전환을 위한 카메라 변수들
    public GameObject mainCamera;
    public GameObject clearCamera;

    //public Stage2_UIManager UIManager;



    void Start () {
        source = GetComponent<AudioSource>();
        // 딜리게이트를 위한 이벤트추가 
        BossControl.OnBossDie += this.OnBossDie;


        StartCoroutine(Stream());
        source.PlayOneShot(bossStageBGM, 0.3f);
    }

    IEnumerator Stream()
    {
        yield return new WaitForSeconds(2.0f);
        // 골렘 함성, 카메라 흔들림(지진)표현 추가하기 
        source.PlayOneShot(golemRoar, 0.3f); 
        yield return new WaitForSeconds(2.0f);

        // 패턴 시작 
        bossControl.Pattern1();
        yield return new WaitForSeconds(8.0f);

        StartCoroutine(CreateMini());
        yield return new WaitForSeconds(5.0f);


        bossControl.Pattern2();
        yield return new WaitForSeconds(6.0f);

        dropStone2.SetActive(true);
        yield return new WaitForSeconds(6.0f);

            
        dropStone1.SetActive(true);
        yield return new WaitForSeconds(6.0f);


        bossControl.Pattern1();
        yield return new WaitForSeconds(6.0f);


        bossControl.Pattern2();
        yield return new WaitForSeconds(6.0f);

        bossControl.Pattern2();
        yield return new WaitForSeconds(6.0f);

        bossControl.Pattern2();
        yield return new WaitForSeconds(6.0f);
    }

   

    // 잡몹 생성 패턴 
    IEnumerator CreateMini()
    {
        while (true)
        {
            CreateMonster.instance.BossStageMon();
            yield return new WaitForSeconds(7.0f);
            CreateMonster.instance.StopCreateMon();

            yield return new WaitForSeconds(15.0f);
        }
    }

    // 보스 hp가 0이 될 경우 딜리게이트 이벤트 발생 
    void OnBossDie()
    {
        StopAllCoroutines();
        StartCoroutine(ChapterClear());
    }

    // 시점 변환,  폭발이펙트 후 보석만 남게 
    IEnumerator ChapterClear() {
        //카메라 전환
        mainCamera.SetActive(false);
        clearCamera.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        dieEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        boss.SetActive(false);
        gem.SetActive(true);

        // Gem 사이즈 점점 작게 

    }
}
