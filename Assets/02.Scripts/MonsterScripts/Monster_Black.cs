using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Black : Monster {
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
            InvalidityAttack();
        }
        else if (coll.gameObject.tag == "BULLET_GREEN")
        {
            InvalidityAttack();
        }
        else if (coll.gameObject.tag == "BULLET_BLUE")
        {
            InvalidityAttack();
        }
        else if (coll.gameObject.tag == "BULLET_BLACK")
        {
MonsterDead();
        }

    }
}
