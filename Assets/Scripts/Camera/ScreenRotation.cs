using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            Debug.Log(Screen.orientation);
            Screen.autorotateToLandscapeLeft = true;
        }
	}

}
