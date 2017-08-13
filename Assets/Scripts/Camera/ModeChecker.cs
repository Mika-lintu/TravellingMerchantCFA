using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModeChecker : MonoBehaviour {

    public void PrintState(GameController.GameState newState)
    {
        Debug.Log(newState);
    }

    public void PrintStateWithText(GameController.GameState newState)
    {
        //Debug.Log("Game Mode Is: " + newState);
    }

}
