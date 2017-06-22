using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameController : MonoBehaviour {

    /*public GameObject player;
    public float gameSpeed;
    public bool moving;
    public bool movingDisabled;*/
    public enum GameState {Free, Battle, Inventory, Event};
    public GameModeEvent gameState;
    public GameState state;

    [Serializable]
    public class GameModeEvent : UnityEvent<GameState> { };

    void Start()
    {
        state = GameState.Battle;
        ChangeMode();
    }

    /*float GameSpeedRelativeToPosition()
    {
        return gameSpeed = Mathf.InverseLerp(9, -14, player.transform.position.y);
    }
    */

    void ChangeMode()
    {
        /*switch (i)
        {
            case 0:
                state = GameState.Free;
                break;
            case 1:
                state = GameState.Battle;
                break;
            case 2:
                state = GameState.Inventory;
                break;
            case 3:
                state = GameState.Event;
                break;
            default:
                break;
        }*/

        gameState.Invoke(state);
    }

}
