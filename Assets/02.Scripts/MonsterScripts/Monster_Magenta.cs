using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Magenta : Monster {
    // 색 별로 달라지는 지점 
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET_CYAN")
        {
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Blue.transform.position = this.transform.position;
            monster_Blue.SetActive(true);
            GameObject TakeCyan = (GameObject)Instantiate(monster_Blue, tr.position, Quaternion.identity);
            // SLIME_CYAN 추적 

        }
        else if (coll.gameObject.tag == "BULLET_MAGENTA")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_YELLOW")
        {
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Red.transform.position = this.transform.position;
            monster_Red.SetActive(true);
            GameObject TakeMagenta = (GameObject)Instantiate(monster_Red, tr.position, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "BULLET_RED")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_GREEN")
        {
            // 검은색으로 
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Black.transform.position = this.transform.position;
            monster_Black.SetActive(true);
            GameObject TakeGreen = (GameObject)Instantiate(monster_Black, tr.position, Quaternion.identity);
            // Green 슬라임 추적 
        }
        else if (coll.gameObject.tag == "BULLET_BLUE")
        {
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_BLACK")
        {
            MonsterDead();
        }

    }
}
