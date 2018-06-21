using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlimeHp : MonoBehaviour {

    //Player의 생명변수
    public int hp = 100;
    //Player의 생명 초깃값 
    private int initHp;
    //Player의 Health bar 이미지 
    public Image imgHpbar;

    public GameObject gameOverPanel;

    public static SlimeHp instance = null;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        imgHpbar.fillAmount = (float)hp / 100;

        if (hp <= 0)
        {
            GameOver();
        }
    }

    public void DecreaseHp(int damage)
    {
        hp -= damage;
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void OnClickReStartBtn()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
