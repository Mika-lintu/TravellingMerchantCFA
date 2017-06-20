using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenchmanAI : MonoBehaviour
{

    BattleController battleC;
    List<GameObject> targetsList;
    GameObject target;
    SpriteRenderer sprite;
    GameSpeed gameSpeed;
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
    bool inReach;
    bool countdownRunning;


    IEnumerator turnCountdown;
    IEnumerator returnToStartPosition;

    delegate void Actions();
    Actions turnActions;

    private void Awake()
    {
        battleC = Camera.main.GetComponent<BattleController>();
        sprite = GetComponent<SpriteRenderer>();
        turnCountdown = runTurnCountdown();
        returnToStartPosition = goToStartPosition();
        gameSpeed = Camera.main.GetComponent<GameSpeed>();
    }


    private void Start()
    {
        //turnActions += Attack;
        stats = GetComponent<Stats>();
        damage = stats.damage;
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
            else GetTargets();
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
        yield return new WaitForSeconds(turnTimer / actionSpeed);
        countdownRunning = false;
        Turn();
    }

    IEnumerator goToStartPosition()
    {
        while(Vector2.Distance(transform.position, startPosition) >= 0.1f)
        {
            gameSpeed.movingDisabled = true;
            transform.position = Vector2.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

            if (startPosition.x < transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            yield return null;
        }
        gameSpeed.movingDisabled = false;
    }

    public void StartBattle()
    {
        StopAllCoroutines();
        battleOngoing = true;
        GetTargets();
        startPosition = (Vector2)transform.position;
        turnTimer = Random.Range(0.5f, 10f);
    }

    public void EndBattle()
    {
        battleOngoing = false;
        inReach = false;
        countdownRunning = false;
        StopAllCoroutines();
        target = null;
        StartCoroutine(goToStartPosition());
    }

    void GetTargets()
    {
        targetsList = battleC.activeEnemies;
        target = null;
        inReach = false;
        countdownRunning = false;
        StopCoroutine(runTurnCountdown());
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
        if (target != null)
        {
            target.GetComponent<Stats>().TakeDamage((int)attackDamage);
            //if (target.GetComponent<Stats>().TakeDamage((int)attackDamage) <= 0)
            //{
            //Debug.Log("Attack: " + target.name + "   Damage: " + attackDamage + "   Target Health: DEAD");
            //target.GetComponent<EnemyAI>().Death();
            /*target = null;
            inReach = false;
            countdownRunning = false;
            StopCoroutine(runTurnCountdown());*/
            //battleC.CheckBattleLists();
            //}
            //else Debug.Log("Attack: " + target.name + "   Damage: " + attackDamage + "   Target Health: " + target.GetComponent<Stats>().health);
        }
    }

    public void Death()
    {
        StopAllCoroutines();
        inReach = false;
        countdownRunning = false;
        gameObject.SetActive(false);
        //stats.Death();
    }
}
