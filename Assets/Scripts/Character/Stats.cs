using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public string characterName;
    public int health;
    public int damage;
    public int level;
    public float dodgeChange;
    public int startHealth; //MIKA does Magic
    int startDamage;
    Color startColor;
    SpriteRenderer sprite;

    BattleController battleC;
    public int characterType;

    BattleUI bUI;
    EnemyDrop enemyDrop;
    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        //startColor = sprite.color;
        battleC = Camera.main.GetComponent<BattleController>();
        bUI = GameObject.FindGameObjectWithTag("BattleUI").GetComponent<BattleUI>();
        enemyDrop = GetComponent<EnemyDrop>();
    }

    private void Start()
    {
        startHealth = health;
        startDamage = damage;
    }

    public void TakeDamage(int damage)
    {
        float hitChange = Random.Range(0f, 1f);
        if (hitChange > dodgeChange)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, startHealth);
            bUI.UpdateHealth(gameObject, damage);
            StartCoroutine(DamageEffect());
        }
        else StartCoroutine(MissEffect());

        if (health <= 0)
        {
            //battleC.CheckBattleLists();
            Death();
            
        }
        //return health;

    }

    public void ItemDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, startHealth);
        bUI.UpdateHealth(gameObject, damage);
        if (health <= 0)
        {
            //battleC.CheckBattleLists();
            Death();
        }
        StartCoroutine(ItemEffect());
    }

    IEnumerator DamageEffect()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = startColor;
    }

    IEnumerator MissEffect()
    {
        sprite.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        sprite.color = startColor;
    }

    IEnumerator ItemEffect()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        sprite.color = startColor;
    }

    public void Death()
    {
        if(characterType == 1)
        {
            enemyDrop.CoinDrop();
        }
        if (gameObject.GetComponent<EnemyAI>() != null)
        {
            //gameObject.GetComponent<EnemyAI>().Death();
            
        }
        else if (gameObject.GetComponent<HenchmanAI>() != null)
        {
            gameObject.GetComponent<HenchmanAI>().Death();
        }
        battleC.CheckBattleLists();
        StopAllCoroutines();
        health = startHealth;
        damage = startDamage;
        sprite.color = startColor;
        bUI.DisableUI(gameObject);
        
        //gameObject.SetActive(false);
    }
}
