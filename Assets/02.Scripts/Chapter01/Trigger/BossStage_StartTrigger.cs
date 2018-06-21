using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossStage_StartTrigger : MonoBehaviour {
    public GameObject NextScenePanel;


    void OnTriggerEnter(Collider call)
    {
        if (call.tag == "SLIME_CYAN" || call.tag == "SLIME_MAGENTA" || call.tag == "SLIME_YELLOW")
        {
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene()
    {
        NextScenePanel.SetActive(true);

        for (int i = 0; i < 50; i++)
        {
            Debug.Log("Changing Black");
            NextScenePanel.GetComponent<Image>().color = Color.Lerp(NextScenePanel.GetComponent<Image>().color, Color.black, Time.deltaTime * 4);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Boss");
    }
}
