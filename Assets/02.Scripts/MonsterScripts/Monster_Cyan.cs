using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Cyan : Monster {
    // 색 별로 달라지는 지점 
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET_CYAN")
        {
            Debug.Log("Cyan hit Cyan");
            MonsterDead();
        }
        else if (coll.gameObject.tag == "BULLET_MAGENTA")
        {
            Debug.Log("Magenta hit Cyan");
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Blue.transform.position = this.transform.position;
            monster_Blue.SetActive(true);
            GameObject TakeCyan = (GameObject)Instantiate(monster_Blue, tr.position, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "BULLET_YELLOW")
        {
            Debug.Log("Yellow hit Cyan");
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Green.transform.position = this.transform.position;
            monster_Green.SetActive(true);
            GameObject TakeMagenta = (GameObject)Instantiate(monster_Green, tr.position, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "BULLET_RED")
        {
            // 검은색으로 
            Destroy(coll.gameObject);
            gameObject.SetActive(false);
            monster_Black.transform.position = this.transform.position;
            monster_Black.SetActive(true);
            GameObject TakeGreen = (GameObject)Instantiate(monster_Black, tr.position, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "BULLET_GREEN")
        {
            MonsterDead();
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
