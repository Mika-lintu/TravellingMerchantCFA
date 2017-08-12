using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject portraitMenu;
    public GameObject landscapeMenu;


    public GameObject[] portraitMenuPages;
    public GameObject[] landscapeMenuPages;

    public GameObject tempText;
    Text temp;

    DeviceOrientation currentOrientation;
    bool portrait;
    int pageNum;

    void Start()
    {
        temp = tempText.GetComponent<Text>();
    }

    void Update()
    {

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            //temp.text = "Orientation = landscapeLeft";
            portrait = false;
            SetLandscapeLayout();
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            //temp.text = "Orientation = portrait";
            portrait = true;
            SetPortraitLayout();
        }
    }


    public void OpenPage(int num)
    {
        pageNum = num;
        if (!portrait)
        {
            landscapeMenuPages[pageNum].SetActive(true);
            temp.text = "" + pageNum;
        }
        else
        {
            portraitMenuPages[pageNum].SetActive(true);
            temp.text = "" + pageNum;
        }
    }


    public void ClosePages()
    {
        if (!portrait)
        {
            for (int i = 0; i < landscapeMenuPages.Length; i++)
            {
                landscapeMenuPages[i].SetActive(false);
            }

        }
        else
        {
            for (int i = 0; i < portraitMenuPages.Length; i++)
            {
                portraitMenuPages[i].SetActive(false);
            }

        }
    }


   

    void SetPortraitLayout()
    {
        if (landscapeMenu.activeInHierarchy)
        {
            landscapeMenuPages[pageNum].SetActive(false);
            landscapeMenu.SetActive(false);
            portraitMenu.SetActive(true);
            portraitMenuPages[pageNum].SetActive(true);
            temp.text = "" + pageNum;
        }
    }

    void SetLandscapeLayout()
    {
        if (portraitMenu.activeInHierarchy)
        {
            portraitMenuPages[pageNum].SetActive(false);
            portraitMenu.SetActive(false);
            landscapeMenu.SetActive(true);
            landscapeMenuPages[pageNum].SetActive(true);
            temp.text = "" + pageNum;
        }
    }

    public void CheckOrientation()
    {
        if (Screen.height > Screen.width)
        {
            portrait = true;
        }
        else
        {
            portrait = false;
        }

        if (portrait)
        {
            portraitMenu.SetActive(true);
        }
        else
        {
            landscapeMenu.SetActive(true);
        }
    }
}
