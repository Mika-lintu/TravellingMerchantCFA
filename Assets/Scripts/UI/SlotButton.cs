using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{

    Image buttonImage;
    Color defaultColor;
    public Color selectColor;


    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultColor = buttonImage.color;
    }

    public void SelectSwitch(bool boo)
    {
            if (boo)
            {
                buttonImage.color = selectColor;
            }
            else
            {
                buttonImage.color = defaultColor;
            }
        

    }
}
