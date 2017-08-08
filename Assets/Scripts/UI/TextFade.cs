using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {
    //Mika
    Text text;
    public float timer;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Color imageColor = text.color;
            imageColor.a -= 0.2f * Time.deltaTime;

            if (imageColor.a <= 0f)
            {
                gameObject.SetActive(false);
            }

            text.color = imageColor;
        } else if (timer > 0f && timer < 4f)
        {
            Color imageColor = text.color;
            

            if (imageColor.a <= 1f)
            {
                imageColor.a += 0.15f * Time.deltaTime;
            }

            text.color = imageColor;
        }
    }
}
