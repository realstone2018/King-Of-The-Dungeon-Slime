using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Yellow : Monster {
    // 색 별로 달라지는 지점 
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET_CYAN")
        {
            Destroy(coll.gameObject);
            gameObject.SetActive(false);

            monster_Green.transform.position = this.transform.position;
            monster_Green.SetActive(true);
            GameObject TakeCyan = (GameObject)Instantiate(monster_Green, tr.position, Quaternion.identity);
            // SLIME_CYAN 추적 

        }
        else if (coll.gameObject.tag == "BULLET_MAGENTA")
        {
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Red.transform.position = this.transform.position;
            monster_Red.SetActive(true);
            GameObject TakeMagenta = (GameObject)Instantiate(monster_Red, tr.position, Quaternion.identity);
            // SLIME_MAGENTA 추적 : 추적은 Red 객체가 할일, Red 타겟을 변경하는 함수 호출하기 
        }
        else if (coll.gameObject.tag == "BULLET_YELLOW")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_RED")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_GREEN")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_BLUE")
        {
            // 검은색으로 
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Black.transform.position = this.transform.position;
            monster_Black.SetActive(true);
            GameObject TakeGreen = (GameObject)Instantiate(monster_Black, tr.position, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "BULLET_BLACK")
        {
            MonsterDead();
        }

    }
}
