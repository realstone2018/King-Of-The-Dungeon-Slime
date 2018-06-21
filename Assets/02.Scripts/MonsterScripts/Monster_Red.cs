using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Red : Monster {
    // 색 별로 달라지는 지점 
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET_CYAN")
        {
            InvalidityAttack();
        }
        else if (coll.gameObject.tag == "BULLET_MAGENTA")
        {
            InvalidityAttack();
        }
        else if (coll.gameObject.tag == "BULLET_YELLOW")
        {
            InvalidityAttack();
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
