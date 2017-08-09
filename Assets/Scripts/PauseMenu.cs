using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject portraitMenu;
    public GameObject landscapeMenu;
    public GameObject[] portraitMenuPages;

    public GameObject[] landscapeMenuPages;

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
}
