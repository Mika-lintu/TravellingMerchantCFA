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
	/*ON UPDATE 
     *      
     * 
     */
	void Update () {

        if (active)
        {
            transform.position = Camera.main.WorldToScreenPoint((Vector3.up * offset) + target.transform.position);
        }
	}

    public void MinusHealth(int damage)
    {
       hp.fillAmount = (float)stats.health / (float)stats.startHealth;
       GetEmptyText(damage);
    }

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
