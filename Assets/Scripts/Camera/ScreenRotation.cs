using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Screen.autorotateToLandscapeLeft == true)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.autorotateToLandscapeLeft = true;
	}
    public void Testi()
    {
        Debug.Log(Screen.orientation);
    }
}
