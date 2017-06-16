using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    private Text popupText;
    RectTransform rTransfomr;
    float timeOfTravel = 2f;
    float currentTime = 0f;
    float normalizedValue;

    Vector3 startPosition = new Vector3(0, 22, 0);
    Vector3 endPosition = new Vector3(0, 100, 0);


    /* ON AWAKE:
     * 
     */
    void Awake()
    {
        popupText = GetComponent<Text>();
        rTransfomr = GetComponent<RectTransform>();
    }
    /* ON POPUPEFFECT (INT):
     *      GETS INT VARIABLE OF DAMAGE TO DISPLAY
     *      IF DAMAGE IS NEGATIVE IT'LL START COURUTINE NAMED HEAL
     *      AND IF DAMAGE IS POSITIVE IT'LL START COURUTINE NAMED DAMAGE
     */
    public void PopupEffect(int dmg)
    {
        if (dmg < 0)
        {
            popupText.text = "" + (-dmg);
            StartCoroutine(Heal());
        }
        else
        {
            popupText.text = "" + dmg;
            StartCoroutine(Damage());
        }
    }
    /*  ON POPUPEFFECT (STRING)
     *      TO BE MADE
     *
     */
    public void PopupEffect(string miss)
    {
       
    }
    /*  ON RESET TEXT:
     *      STOPS ALL COURUTINES 
     *      RESETS POSITION, COLOR AND ALPHA
     */
    public void ResetText()
    {
        StopCoroutine(Damage());
        StopCoroutine(Heal());
        transform.position = transform.parent.position;
        gameObject.SetActive(false);
        currentTime = 0;
        Color imageColor = popupText.color;
        imageColor.a = 1f;
        popupText.color = imageColor;
        StopAllCoroutines();
    }
    /*  ON DAMAGE IENUMERATOR:
     *      TRANSFORMS POSITION TO MOVE UP     
     *      SETS COLOR RED
     */
    IEnumerator Damage()
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel;
            rTransfomr.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normalizedValue);
            Color imageColor = popupText.color;
            imageColor = Color.red;
            imageColor.a -= 0.5f * Time.deltaTime;
            popupText.color = imageColor;
            yield return null;
        }
        ResetText();
    }
    /*  ON HEAL IENUMERATOR 
     *      TRANSFORM POSITION TO MOVE UP
     *      SETS COLOR GREEN
     */
    IEnumerator Heal()
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel;
            rTransfomr.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normalizedValue);
            Color imageColor = popupText.color;
            imageColor = Color.green;
            imageColor.a -= 0.5f * Time.deltaTime;
            popupText.color = imageColor;
            yield return null;
        }
        ResetText();
    }
}
