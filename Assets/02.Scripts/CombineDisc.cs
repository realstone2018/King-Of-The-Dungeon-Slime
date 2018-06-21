using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineDisc : MonoBehaviour {
    public Image cyanDisc;
    public Image magentaDisc;
    public Image yellowDisc;

    public Color initColor;
    void Start()
    {
        initColor = cyanDisc.color;
    }
    // z 눌렀을 때 전체가 ??


    // 123 에 따른 각 디스크 ???
    public void DiscBrighten(string color)
    {
        if (color == "Cyan")
            cyanDisc.color = new Color(255, 255, 255);
        else if (color == "Magenta")
            magentaDisc.color = new Color(255, 255, 255);
        else
            yellowDisc.color = new Color(255, 255, 255);
    }

    public void ReSetCombineDisc()
    {
        cyanDisc.color = initColor;
        magentaDisc.color = initColor;
        yellowDisc.color = initColor;
    }

}
