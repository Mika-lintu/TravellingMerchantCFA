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


    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public bool targetInRange;
    [HideInInspector] public float distanceToTarget;

    private bool aiActive;

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
        aiActive = true;
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


    public bool TargetInRange()
    {
        return DistanceToTarget() <= aiStats.range;
    }


    public void ResetStateTimer()
    {
        stateTimeElapsed = 0f;
    }

    public float DistanceToTarget()
    {
        distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
        return Vector2.Distance(transform.position, currentTarget.position);
    }


    public void Attack()
    {
        ResetStateTimer();
    }


    public void SetMoveToTargetState()
    {
        TransitionToState(moveToTargetState);
    }


    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, aiStats.movementSpeed * Time.deltaTime);
    }

}
