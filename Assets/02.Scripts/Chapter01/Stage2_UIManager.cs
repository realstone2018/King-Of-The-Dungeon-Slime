using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_UIManager : MonoBehaviour {

    public GameObject tutorial;
    public GameObject line1;
    public GameObject line2;

    // 혼합 튜토리얼
    public void TutorialTwoRedUI()
    {
        StartCoroutine(MixSlimeRed());
    }

    // 클리어
    public void ClearTutorial()
    {
        tutorial.SetActive(false);
    }
    // 실패
    public void FalseTutorialTwo()
    {
        line1.SetActive(false);
        line2.SetActive(true);
    }


    IEnumerator MixSlimeRed()
    {
        tutorial.SetActive(true);
        yield return null;
    }
   


    /*
   public void TutorialTwoGreenUI()
   {
       StartCoroutine(MixSlimeGreen());
   }

   public void TutorialTwoBlueUI()
   {
       StartCoroutine(MixSlimeBlue());
   }

   IEnumerator MixSlimeGreen()
   {
       // 저녀석들은 지금의 우리들론 상대 할 수 없어 
       // z 1 + 2 를 눌러봐 
       yield return null;
   }

   IEnumerator MixSlimeBlue()
   {
       // 저녀석들은 지금의 우리들론 상대 할 수 없어 
       // z 1 + 2 를 눌러봐 
       yield return null;
   }
   */
}
