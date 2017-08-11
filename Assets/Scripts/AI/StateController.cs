using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public AIStats aiStats;
    public Transform currentTarget;
    public State remainState;
    public State turnState;
    public State moveToTargetState;
    private bool aiActive;

    AnimationControl animControl;
    IEnumerator currentCoroutine = null;

    [HideInInspector]
    public float stateTimeElapsed;
    [HideInInspector]
    public bool targetInRange;
    [HideInInspector]
    public float distanceToTarget;
    [HideInInspector]
    public Dictionary<string, int> turnActions;

    #region General

    private void Awake()
    {
        animControl = GetComponent<AnimationControl>();
    }

    private void Start()
    {
        SetupAI();

    }


    private void Update()
    {
        if (!aiActive)
        {
            return;
        }
        else
        {
            currentState.UpdateState(this);
        }

    }


    public void SetupAI()
    {
        turnActions = aiStats.GetActionsDictionary();
        aiActive = true;
    }


    public void PickTurnAction()
    {
        int oddsSum = 0;
        float odd = Random.value;
        float tempOdd = 0f;
        string action = "";
        List<string> actionString = new List<string>();
        List<float> actionOdd = new List<float>();


        foreach (KeyValuePair<string, int> values in turnActions)
        {
            actionString.Add(values.Key);
            oddsSum += values.Value;
        }


        foreach (KeyValuePair<string, int> values in turnActions)
        {
            actionOdd.Add((float)values.Value / (float)oddsSum);
        }


        for (int i = 0; i < actionOdd.Count; i++)
        {
            if (i == actionOdd.Count - 1)
            {
                action = actionString[i];
                Invoke(action, 0f);
                return;
            }
            else
            {
                if (odd >= tempOdd && odd <= tempOdd + actionOdd[i])
                {
                    action = actionString[i];
                    Invoke(action, 0f);
                    return;
                }
                else
                {
                    tempOdd += actionOdd[i];
                }
            }
        }
    }


    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }


    public bool TurnTimerHasElapsed()
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= aiStats.turnSpeed);
    }


    public void ResetStateTimer()
    {
        stateTimeElapsed = 0f;
    }


    #endregion

    #region Turn Actions

    public void MeleeAttack()
    {
        animControl.SetAnimation(animControl.attack, false);
        ResetStateTimer();
    }

    public void Steal()
    {
        Debug.Log("Steal");
        ResetStateTimer();
    }

    public void RangedAttack()
    {
        Debug.Log("RangedAttack");
        ResetStateTimer();
    }

    public void Flee()
    {
        Debug.Log("Flee");
        ResetStateTimer();
    }

    public void SpecialAttack()
    {
        Debug.Log("Special Attack");
        ResetStateTimer();
    }

    public void HealSelf()
    {
        Debug.Log("HealSelf");
        ResetStateTimer();
    }

    #endregion

    #region Movement & Range Stuff

    public float DistanceToTarget()
    {
        distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
        return Vector2.Distance(transform.position, currentTarget.position);
    }

    public void SetMoveToTargetState()
    {
        targetInRange = false;
        TransitionToState(moveToTargetState);
    }

    public void MoveToTarget()
    {
        if (currentCoroutine == null || currentCoroutine.ToString() != MoveCoroutine().ToString())
        {
            StopAllCoroutines();
            currentCoroutine = MoveCoroutine();
            StartCoroutine(currentCoroutine);
        }
    }

    IEnumerator MoveCoroutine()
    {
        animControl.SetAnimation(animControl.walk, true);

        while (!targetInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, aiStats.movementSpeed * Time.deltaTime);
            yield return null;
        }
        animControl.SetAnimation(animControl.idle, true);
    }

    public bool TargetInRange()
    {
        return DistanceToTarget() <= aiStats.range;
    }

    #endregion
}
