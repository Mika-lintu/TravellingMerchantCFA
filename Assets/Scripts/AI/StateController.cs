using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public float health;
    public State currentState;
    public AIStats aiStats;
    public Transform currentTarget;
    public State remainState;
    public State turnState;
    public State moveToTargetState;
    public State fleeState;
    public State deathState;
    private bool aiActive;

    AnimationControl animControl;
    IEnumerator currentCoroutine = null;

    [HideInInspector]
    public float stateTimeElapsed;
    [HideInInspector]
    public bool targetInRange;
    [HideInInspector]
    public bool fleeing;
    [HideInInspector]
    public float distanceToTarget;
    [HideInInspector]
    public Dictionary<string, int> turnActions;
    [HideInInspector]
    public GameObject fleeTarget;
    [HideInInspector]
    public bool isFlipped = false;

    #region General

    private void Awake()
    {
        animControl = GetComponent<AnimationControl>();
        fleeing = false;
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
        health = aiStats.maxHealth;
        fleeTarget = GameObject.FindGameObjectWithTag("FleeTarget");
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
        currentTarget = fleeTarget.transform;
        targetInRange = false;
        TransitionToState(fleeState);
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

    public void Death()
    {
        Debug.Log("Death");
        currentTarget = null;
        animControl.DeathAnimation();
        TransitionToState(deathState);
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


    public void FleeFromBattle()
    {
        if (currentCoroutine == null || currentCoroutine.ToString() != FleeCoroutine().ToString())
        {
            StopAllCoroutines();
            currentCoroutine = FleeCoroutine();
            StartCoroutine(currentCoroutine);
        }
    }


    IEnumerator MoveCoroutine()
    {
        FlipCharacter();
        animControl.SetAnimation(animControl.walk, true);

        while (!targetInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, aiStats.movementSpeed * Time.deltaTime);
            yield return null;
        }
        animControl.SetAnimation(animControl.idle, true);
        currentCoroutine = null;

    }


    IEnumerator FleeCoroutine()
    {
        FlipCharacter();
        animControl.SetAnimation(animControl.steal, true);

        while (!targetInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, aiStats.movementSpeed * Time.deltaTime);
            yield return null;
        }
        currentCoroutine = null;

    }


    public bool TargetInRange()
    {
        return DistanceToTarget() <= aiStats.range;
    }


    void FlipCharacter()
    {
        if (currentTarget.position.x > transform.position.x)
        {
            if (!isFlipped)
            {
                isFlipped = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                if (isFlipped)
                {
                    isFlipped = false;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                }
            }
        }
    }

        #endregion
    
}
