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
    bool freezeMovement;
    bool playerMoving;

    [Serializable]
    public class GameModeEvent : UnityEvent<GameState> { };

    public UnityEvent playerIsMoving;
    public UnityEvent playerHasStopped;

    void Start()
    {
        state = GameState.Free;
        battleOnGoing = false;
        playerMoving = false;
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
                ChangeMode(stateInput);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                stateInput = 1;
                ChangeMode(stateInput);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                stateInput = 2;
                ChangeMode(stateInput);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                stateInput = 3;
                ChangeMode(stateInput);
            }
            else if (Input.GetKeyDown("a"))
            {
                ToggleMovement();
            }

        }

    }

    private void OnEnable()
    {
        BattleController.EndBattle += EndBattle;
    }

    private void OnDisable()
    {
        BattleController.EndBattle -= EndBattle;
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
                battleOnGoing = true;
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
            freezeMovement = true;
            state = GameState.Battle;
        }
        gameState.Invoke(state);
        ToggleMovement();
    }

    void EndBattle()
    {
        battleOnGoing = false;
        freezeMovement = false;
        ChangeMode(0);
    }

    void ToggleMovement()
    {
        if (state == GameState.Free)
        {
            if (playerMoving)
            {
                playerMoving = false;
                playerHasStopped.Invoke();
            }
            else
            {
                playerMoving = true;
                playerIsMoving.Invoke();
            }
        }
        else
        {
            playerMoving = false;
            playerHasStopped.Invoke();
        }
    }

}
