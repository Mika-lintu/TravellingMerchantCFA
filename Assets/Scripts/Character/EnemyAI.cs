using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /*
    BattleController battleC;
    List<GameObject> targetsList;
    GameObject target;
    SpriteRenderer sprite;
    Vector2 startPosition;
    Stats stats;
    float distanceToTarget;
    public float reach;
    public float movementSpeed;
    public float actionSpeed;
    public bool battleOngoing;
    float fixedActionSpeed;
    float damage;
    float turnTimer = 10f;
    bool flipSprite;
    public bool inReach;
    public bool countdownRunning;

    public AudioClip attackSound1;
    public AudioClip attackSound2;

    IEnumerator turnCountdown;

    delegate void Actions();
    Actions turnActions;

   

    private void Awake()
    {
        battleC = Camera.main.GetComponent<BattleController>();
        sprite = GetComponent<SpriteRenderer>();
        turnCountdown = runTurnCountdown();
        startPosition = transform.position;
        stats = GetComponent<Stats>();
    }


    private void Start()
    {
        turnActions += Attack;
        damage = GetComponent<Stats>().damage;
    }
    private void Update()
    {
        if (battleOngoing)
        {
            if (target != null)
            {
                distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

                if (distanceToTarget <= reach)
                {
                    inReach = true;
                    InReach();
                }
                else
                {
                    inReach = false;
                    Movement();
                }
            }

        }
    }

    private void OnEnable()
    {
        BattleController.UpdateTargets += GetTargets;
        BattleController.EndBattle += EndBattle;
        BattleController.StartBattle += StartBattle;
        turnActions += Attack;
    }

    private void OnDisable()
    {
        BattleController.UpdateTargets -= GetTargets;
        BattleController.EndBattle -= EndBattle;
        BattleController.StartBattle -= StartBattle;
    }

    void InReach()
    {
        if (!countdownRunning)
        {
            fixedActionSpeed = actionSpeed + Random.Range(-0.5f, 0.5f);
            turnCountdown = runTurnCountdown();
            StartCoroutine(turnCountdown);
        }
    }

    void EndBattle()
    {
        battleOngoing = false;
        StopAllCoroutines();
    }

    void Movement()
    {
        if (!inReach)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
            transform.position = newPosition;
        }

        if (target.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void Turn()
    {
        turnTimer = 10f;
        if (turnActions != null)
        {
            turnActions();
        }
    }

    IEnumerator runTurnCountdown()
    {
        countdownRunning = true;
        yield return new WaitForSeconds(turnTimer / fixedActionSpeed);
        countdownRunning = false;
        Turn();
    }

    public void StartBattle()
    {
        turnTimer = Random.Range(0.5f, 10f);
        battleOngoing = true;
        GetTargets();
    }

    void GetTargets()
    {
        targetsList = battleC.goodGuys;
        float nearest = 100f;

        for (int i = 0; i < targetsList.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, targetsList[i].transform.position);

            if (distance < nearest)
            {
                nearest = distance;
                target = targetsList[i];
            }
        }
    }

    void Attack()
    {
        float attackDamage = Random.Range(damage - 3, damage + 3);
        attackDamage = Mathf.RoundToInt(attackDamage);
        target.GetComponent<Stats>().TakeDamage((int)attackDamage);
        /*if (target.GetComponent<Stats>().TakeDamage((int)attackDamage) <= 0)
        {
            if (target.name == "Player")
            {
                battleC.PlayerDeath();
            }
            else
            {
                target.GetComponent<HenchmanAI>().Death();
                StopAllCoroutines();
                countdownRunning = false;
                inReach = false;
                target = null;
                battleC.CheckBattleLists();
            }

        //Debug.Log("Attack: " + target.name + "   Damage: " + attackDamage + "   Target Health: " + target.GetComponent<Stats>().health);
    }

    public void Death()
    {
        StopAllCoroutines();
        countdownRunning = false;
        inReach = false;
        //stats.Death();
        transform.position = startPosition;
        target = null;
        gameObject.SetActive(false);
    }
    */
}