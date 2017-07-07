using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameController : MonoBehaviour
{
    public enum GameState { Free = 0, Battle = 1, Inventory = 2, Event = 3 };
    public GameModeEvent gameState;
    public GameState state;
    int stateInput = 0;

    [Serializable]
    public class GameModeEvent : UnityEvent<GameState> { };

    void Start()
    {
        state = GameState.Free;
    }

    void Update()
    {
        if (!Input.anyKeyDown)
        {
            //do nothing
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                stateInput = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                stateInput = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                stateInput = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                stateInput = 3;
            }
            ChangeMode(stateInput);
        }

    }


    void ChangeMode(int modeInput)
    {
        switch (modeInput)
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
        }

        gameState.Invoke(state);
    }

}
