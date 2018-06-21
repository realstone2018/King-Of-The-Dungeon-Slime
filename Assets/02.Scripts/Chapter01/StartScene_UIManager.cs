using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartScene_UIManager : MonoBehaviour {
    // 패널오브젝트들 
    public GameObject titlePanel;
    public GameObject case1Panel;
    public GameObject case2Panel;
    public GameObject case3Panel;
    public GameObject revolutionPanel;
    public GameObject reportPanel;
    public GameObject roadingPanel;
    public GameObject revolutionTextObj;
    // 특수효과를 위한 오브젝트들
    public Image titleImage;
    public Image case1Text;
    public Image case2Text;
    public Image case3Text;
    public Image whiteImage;
    public Image revolutionBtn;
    public Image roadingBar;
    public GameObject stamp;
    public Image revolutionTextImg;
    //사운드 재생을 위한 변수들 
    private AudioSource source = null;
    public AudioClip puckClip;
    public AudioClip typingClip;
    public AudioClip crowdCryClip;
    public AudioClip paperClip;
    public AudioClip stampClip;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnClickStartBtn()
    {
        StartCoroutine(Stream());
    }

    public void OnClickRevolutionBtn()
    {
        source.PlayOneShot(crowdCryClip, 1.0f);
        // 화면 전환효과 재생 
        StartCoroutine(Revolution());
    }

    public void OnClikeOperationStartBtn()
    {
        // 화면 전환 후 씬 전환 
        StartCoroutine(OperationSTart());
    }

    IEnumerator Stream()
    {
        // 이미지의 칼라가 투명이 될 때까지 보간한다.  FadeOut 기능
        for (int i = 0; i < 30; i++)
        {
            titleImage.color = Color.Lerp(titleImage.color, Color.clear, Time.deltaTime * 4);
            yield return new WaitForSeconds(0.1f);
        }

        // 타이틀패널이 사라지면서 뒤에있던 case1패널이 등장 
        source.PlayOneShot(puckClip, 1.0f);
        titlePanel.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        // Text 글자 왼쪽에서 오른쪽으로 나타내기 
        // 타자치는 소리 재생 
        source.PlayOneShot(typingClip, 1.0f);
        for (int i = 0; i <= 100; i++) {
            case1Text.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);

        //  case1패널이 사라지면서 뒤에있던 case2패널이 등장 
        source.PlayOneShot(puckClip, 1.0f);
        case1Panel.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        source.PlayOneShot(typingClip, 1.0f);
        for (int i = 0; i <= 100; i++)
        {
            case2Text.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);

        //  case2패널이 사라지면서 뒤에있던 case3패널이 등장 
        source.PlayOneShot(puckClip, 1.0f);
        case2Panel.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        source.PlayOneShot(typingClip, 1.0f);
        for (int i = 0; i <= 100; i++)
        {
            case3Text.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);

        // case3패널 비활성화
        case3Panel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= 100; i++)
        {
            revolutionTextImg.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Revolution()
    {
        // 텍스트는삭제하고 화면을 점점 밝게 
        revolutionTextObj.SetActive(false);
        
        // 이미지의 칼라가 투명이 될 때까지 보간한다.  FadeOut 기능
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = Color.Lerp(whiteImage.color, Color.white, Time.deltaTime * 4);
            revolutionBtn.color = Color.Lerp(revolutionBtn.color, Color.clear, Time.deltaTime * 4);
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(2.0f);
        // 투명해진 Panel을 비활성화
        revolutionPanel.SetActive(false);
        source.PlayOneShot(paperClip, 1.0f);
    }

    IEnumerator OperationSTart()
    {
        source.PlayOneShot(stampClip, 1.0f);
        stamp.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        // Report패널이 사라지고 Roading화면 출력
        reportPanel.SetActive(false);
        // 로딩바 진행, 잠시 멈추었다가 다시 진행 
        for (int i = 0; i <= 30; i++)
        {
            roadingBar.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);
        for (int i = 30; i <= 100; i++)
        {
            roadingBar.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);

        // 다음 씬으로 넘기기 
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Chapter01");
    }
}
