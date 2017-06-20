using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public List<GameObject> textList;
    Stats stats;
    public Image hp;
    public GameObject target;
    public float offset;
    private bool active;

    /*
     * SET UI TO OBJECT
     *      GETS GAMEOJECT TO TARGET (BATTLE UI SCRIPT TELLS WHAT GAMEOBJECT TO TARGET)
     *      GETS TARGETS HEALTH AMOUNT FROM STATS SCRIPTS (STAT SCRIPT IS IN TARGET GAMEOBJECT)
     *      UPDATES HEALHTBAR FILL TO CORRECT FROM STATS
     */
    public void SetUIToObject(GameObject go)
    {
        target = go;
        active = true;
        stats = go.GetComponent<Stats>();
        hp.fillAmount = (float)stats.health / (float)stats.startHealth;
    }
	/* ON UPDATE 
     *      GETS UISETS POSITION ON FROM CANVAS AND TRANSFERS IT TO SCENE POSITION
     *      IT GETS TARGET GAMEOBJECTS POSITION ADDS OFFSET AND SO FOLLOWS TARGET
     */
	void Update () {

        if (active)
        {
            transform.position = Camera.main.WorldToScreenPoint((Vector3.up * offset) + target.transform.position);
        }
	}

    /* ON HEALTH UPDATE:
     *      GETS INT VARIABLE AND CHANCES IT TO FLOAT 
     *      WITH FLOAT VARIABLE IT CHANCES HPBARS FILL AMOUNT
     *      CALLS GETEMPTYTEXT FUNCTION AND GIVES IT INT VARIABLE
     */
    public void HealthUpdate(int damage)
    {
       hp.fillAmount = (float)stats.health / (float)stats.startHealth;
       GetEmptyText(damage);
    }

    /* ON RESET HPBAR:
     *      FILLS HPBARS FILL AMOUNT
     *      DIABLES TEXT ITEMS IF ACTIVE
     *      AND SETS TEXT ITEMS POSITION BACK TO PARENT
     */
    public void ResetHPBar()
    {
        hp.fillAmount = 1f;
        for (int i = 0; i < textList.Count; i++)
        {
            if (textList[i].activeInHierarchy)
            {
                textList[i].SetActive(false);
                transform.position = transform.parent.position;
            }
        }
        
    }

    /* ON GET EMPTY TEXT:
     *      GETS TEXT ITEM THAT IS NOT ACTIVE IN HIERARCHY
     *      SETS IT ACTIVE AND CALLS POPUPEFFECT FUNCTION
     */
    public void GetEmptyText(int dmg)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            if (!textList[i].activeInHierarchy)
            {
                textList[i].SetActive(true);
                textList[i].GetComponent<TextEffect>().PopupEffect(dmg);
                break;
            }
        }
    }
    
}
