using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour {
    /*
    enum CharacterType { Henchman, Enemy }
    CharacterType characterType = new CharacterType();

    delegate void TurnActions();
    Dictionary<string, List<TurnActions>> turnActions = new Dictionary<string, List<TurnActions>>();
    BattleController battleController = new BattleController();
    List<GameObject> allTargets = new List<GameObject>();
    GameObject currentTarget;
    NPCStats characterStats;

    float reach;
    float movementSpeed;
    float turnSpeed;
    float turnTimer;
    bool inReach = false;
    bool lowHealth = false;


    void Awake()
    {
        battleController = Camera.main.GetComponent<BattleController>();
        characterStats = GetComponent<NPCStats>();
    }

    #region Start of the battle

    public void GetStats()
    {
        int charType = 0;
        characterStats.GetStats(out reach, out movementSpeed, out turnSpeed, out charType);
        characterType = (CharacterType)charType;
    }


    void SetCharacterActions(List<string> actionStrings)
    {

        for (int i = 0; i < actionStrings.Count; i++)
        {
            
        }
    }


    public void GetAllTargets()
    {
        if (characterType == CharacterType.Enemy)
        {
            allTargets = battleController.goodGuys;
        }
        else if (characterType == CharacterType.Henchman)
        {
            allTargets = battleController.baddies;
        }

        ChooseClosestTarget();
    }


    void ChooseClosestTarget()
    {
        float distanceToTarget = 0f;

        for (int i = 0; i < allTargets.Count; i++)
        {
            float tempDistance = Vector2.Distance(transform.position, allTargets[i].transform.position);

            if (tempDistance < distanceToTarget)
            {
                currentTarget = allTargets[i];
                distanceToTarget = tempDistance;
            }
        }

        if (distanceToTarget > reach)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToTarget());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Turn());
        }
    }

    #endregion


    IEnumerator Turn()
    {
        turnTimer = turnSpeed + Random.Range(-2f, 2f);

        while (turnTimer > 0f)
        {
            turnTimer -= Time.deltaTime;
        }
        
        yield return null;
    }
 
    IEnumerator MoveToTarget()
    {
        float distanceToTarget = Vector2.Distance(currentTarget.transform.position, transform.position);
        while(distanceToTarget > reach)
        {
            distanceToTarget = Vector2.Distance(currentTarget.transform.position, transform.position);
            Vector2.MoveTowards(transform.position, currentTarget.transform.position, movementSpeed * Time.deltaTime);
        }

        inReach = true;
        yield return null;
    }

    #region Actions

    void MeleeAttack()
    {

    }


    void RangedAttack()
    {

    }


    void Flee()
    {

    }


    void Steal()
    {

    }


    #endregion

    */
}
