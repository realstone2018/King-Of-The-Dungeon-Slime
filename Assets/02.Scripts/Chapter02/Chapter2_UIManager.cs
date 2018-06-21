using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2_UIManager : MonoBehaviour
{
    public GameObject openningPanel;
    public Text openningText;

    public void StartOpenning()
    {
        StartCoroutine(StageOpenning());   
    }

    IEnumerator StageOpenning()
    {
        for (int i = 0; i < 30; i++)
        {
            openningPanel.GetComponent<Image>().color = Color.Lerp(openningPanel.GetComponent<Image>().color, Color.clear, 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 50; i++)
        {
            openningText.color = Color.Lerp(openningText.color, Color.clear, 0.05f);
            yield return new WaitForSeconds(0.1f);
        }

        openningPanel.SetActive(false);
    }


}
