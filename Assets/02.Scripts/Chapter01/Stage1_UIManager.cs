using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1_UIManager : MonoBehaviour {
    // 패널들 
    //public GameObject cyanHpPanel;
    public GameObject dDayPanel;
    public GameObject Tutorial_LIne1Panel;

    // 특수효과를 위한 이미지들 
    public Image dDayText1;
    public Image dDayText2;
    public GameObject tutorialText1;
    public GameObject tutorialText2;
    public bool finishOpenning = false;

    // Stage1_Manager에서 호출용 메소드들  
    public void OpenStage()
    {
        StartCoroutine(StageOpenning());
    }

    public void FalseTutorialOne()
    {
        tutorialText1.SetActive(false);
        tutorialText2.SetActive(true);
    }

    public void ClearTutorial()
    {
        Tutorial_LIne1Panel.SetActive(false);

    }

    IEnumerator StageOpenning()
    {
        for (int i = 0; i <= 100; i++)
        {
            dDayText1.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i <= 100; i++)
        {
            dDayText2.fillAmount = (float)i / 100;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.0f);


        // DdayPanel이 사라지면서 게임씬 등장 
        for (int i = 0; i < 25; i++)
        {
            dDayPanel.GetComponent<Image>().color = Color.Lerp(dDayPanel.GetComponent<Image>().color, Color.clear, Time.deltaTime * 4);
            dDayText1.color = Color.Lerp(dDayText1.color, Color.clear, Time.deltaTime * 4);
            dDayText2.color = Color.Lerp(dDayText1.color, Color.clear, Time.deltaTime * 4);
            yield return new WaitForSeconds(0.08f);
        }
        dDayPanel.SetActive(false);

        Tutorial_LIne1Panel.SetActive(true);
    }

}
