using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour {

    // 오브젝트 UI를 이용해 위에 HP표시 
    public int hp = 10;
    //Player의 Health bar 이미지 
    public Image imgHpbar;

    public bool setStatue = false;
    public GameObject hpBarPanel;

    public delegate void DestoryStatueHandler();
    public static event DestoryStatueHandler OnDestroyStatue;

    void OnCollisionEnter(Collision coll)
    {
        if (setStatue)
        {
            if (coll.gameObject.tag == "BULLET_CYAN" || coll.gameObject.tag == "BULLET_MAGENTA" || coll.gameObject.tag == "BULLET_YELLOW" || coll.gameObject.tag == "BULLET_RED" || coll.gameObject.tag == "BULLET_GREEN" || coll.gameObject.tag == "BULLET_BLUE" || coll.gameObject.tag == "BULLET_BLACK")
            {
                Destroy(coll.gameObject);

                hp--;
                //Image UI 항목의 fillAmount 속성을 조절해 생명 게이지 값 조절 
                imgHpbar.fillAmount = (float)hp / 10f;
            }

            if (hp <= 0)
            {
                DestroyStatue();
            }
            // 네이버게이션 비활성화 , 네임스페이스 추가한 후 작성하기 
            //초기화 
        }
    }

    public void AppearHpbar()
    {
        hpBarPanel.SetActive(true);
    }

    void DestroyStatue()
    {
        OnDestroyStatue();
    }
}
