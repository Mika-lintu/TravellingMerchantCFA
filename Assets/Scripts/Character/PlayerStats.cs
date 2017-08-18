using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats {

    public int currentSegment;
    

    public string GetString()
    {
        return "\"currentSegment\": \"" + currentSegment;
    }
	
}
