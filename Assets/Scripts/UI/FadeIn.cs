using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    Image image;
    public float timer = 2f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Color imageColor = image.color;
            imageColor.a -= 0.25f * Time.deltaTime;

            if (imageColor.a <= 0f)
            {
                gameObject.SetActive(false);
            }

            image.color = imageColor;

        }
    }

}
