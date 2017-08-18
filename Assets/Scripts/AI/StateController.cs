using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private float timerVariable;
    private float attackVariable;
    StateController targetStateController;
    AnimationControl animControl;
    BattleController battleControl;
    IEnumerator currentCoroutine = null;
    public UnityEvent deathEvent;
    GameObject player;

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
    Transform fleeTarget;
    [HideInInspector]
    Vector3 fleeTargetPosition;
    [HideInInspector]
    public bool isFlipped = false;
    [HideInInspector]
    public bool isPlayer;
    public enum AIType { hostile, friendly, player };
    public AIType entityType;



    #region General

    private void Awake()
    {
        animControl = GetComponent<AnimationControl>();
        battleControl = Camera.main.GetComponent<BattleController>();
        player = GameObject.FindGameObjectWithTag("Player");
        fleeing = false;
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

        
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
            health = aiStats.maxHealth;
        }

        if (entityType == AIType.hostile)
        {
            fleeTarget = transform.GetChild(0);
            ResetPosition();
            FindTarget();
            turnActions = aiStats.GetActionsDictionary();
            timerVariable = aiStats.turnSpeed * 0.5f;
            attackVariable = aiStats.damage * 0.3f;
            Debug.Log(currentTarget.name);
            currentTarget.gameObject.GetComponent<StateController>().AddDeathListener(gameObject);
        }

        aiActive = true;
    }


    public void StartNewBattle()
    {
        SetupAI();
    }


    public void ResetPosition()
    {
        float randomX = Random.value;
        float randomY = Random.Range(-3, 5);

        if (randomX <= 0.5f)
        {
            randomX = -30f;
        }
        else
        {
            randomX = 30f;
        }

        transform.position = new Vector3(randomX, randomY, transform.position.z);
        fleeTargetPosition = transform.position;
    }


    public void FindTarget()
    {
        List<GameObject> tempTargetList = new List<GameObject>();
        GameObject tempTarget = player.gameObject;
        float distanceToTarget = 100;

        if (entityType == AIType.friendly)
        {
            tempTargetList = battleControl.overallEnemies;
        }
        else if (entityType == AIType.hostile)
        {
            tempTargetList = battleControl.goodGuys;
        }

        for (int i = 0; i < tempTargetList.Count; i++)
        {
            float tempDistance = Vector2.Distance(transform.position, tempTargetList[i].transform.position);

            if (tempDistance < distanceToTarget)
            {
                distanceToTarget = tempDistance;
                tempTarget = tempTargetList[i];
            }
        }

        currentTarget = tempTarget.transform;
        targetStateController = currentTarget.GetComponent<StateController>();
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
        stateTimeElapsed = Random.Range(0, timerVariable);
    }


    #endregion

    #region Turn Actions

    public void TakeDamage(float damage)
    {
        health -= damage;
        animControl.SetAnimation(animControl.hurt, false);
    }

    public void MeleeAttack()
    {
        animControl.SetAnimation(animControl.attack, false);
        float damage = Random.Range(aiStats.damage - attackVariable, aiStats.damage + attackVariable);
        targetStateController.TakeDamage(Mathf.Round(damage));
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
        fleeTarget.position = fleeTargetPosition;
        fleeTarget.transform.parent = transform.parent;
        currentTarget = fleeTarget;
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
        deathEvent.Invoke();
        TransitionToState(deathState);
    }

    public void AddDeathListener(GameObject go)
    {
        deathEvent.AddListener(go.GetComponent<StateController>().TargetIsDead);
    }


    public void TargetIsDead()
    {
        Flee();
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
        currentTarget = fleeTarget;

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
