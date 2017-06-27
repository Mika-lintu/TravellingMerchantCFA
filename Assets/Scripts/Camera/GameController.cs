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
    bool battleOnGoing;

    [Serializable]
    public class GameModeEvent : UnityEvent<GameState> { };

    void Start()
    {
        state = GameState.Battle;
        battleOnGoing = false;
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
                SetBattleOngoing(true);
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

        CheckModeTerms();

    }

    void CheckModeTerms()
    {
        if (!battleOnGoing)
        {
            // Do nothing;
        }
        else if (state == GameState.Free || state == GameState.Event)
        {
            state = GameState.Battle;
        }
        gameState.Invoke(state);
    }

    public void SetBattleOngoing(bool i)
    {
        if (i)
        {
            battleOnGoing = true;
        }
        else
        {
            battleOnGoing = false;
            ChangeMode(0);
        }
    }

}
