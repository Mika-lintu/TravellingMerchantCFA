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
    
    void Awake()
    {
        popupText = GetComponent<Text>();
        rTransfomr = GetComponent<RectTransform>();
    }

    public void PopupEffect(int dmg)
    {
        StopAllCoroutines();
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
    public void PopupEffect(string miss)
    {
       
    }
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
