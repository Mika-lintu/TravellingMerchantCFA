using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject portraitMenu;
    public GameObject landscapeMenu;

    public GameObject[] portraitMenuPages;
    public GameObject[] landscapeMenuPages;

    bool portrait;

    void Start () {
		
	}
	
	public void ClosePagesPortrait()
    {
        for (int i = 0; i < portraitMenuPages.Length; i++)
        {
            portraitMenuPages[i].SetActive(false);
        }
    }


    public void ClosePagesLandscape()
    {
        for (int i = 0; i < landscapeMenuPages.Length; i++)
        {
            landscapeMenuPages[i].SetActive(false);
        }
    }
    public void CheckOrientation()
    {
        if(Screen.height > Screen.width)
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
        }else
        {
            landscapeMenu.SetActive(true);
        }
    }
}
