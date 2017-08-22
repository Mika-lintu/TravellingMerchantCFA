using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScreen : MonoBehaviour {
    public GameObject landscapeScreen;
    public GameObject portraitSreen;

    SceneLoad sceneLoad;

	void Start () {
        sceneLoad = GetComponent<SceneLoad>();
	}
	
	void Update () {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            LandscapeLayout();
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            PortraitLayout();
        }
       
    }
    void PortraitLayout()
    {
        landscapeScreen.SetActive(false);
        portraitSreen.SetActive(true);
    }
    public void LandscapeLayout()
    {
        portraitSreen.SetActive(false);
        landscapeScreen.SetActive(true);
    }
}
