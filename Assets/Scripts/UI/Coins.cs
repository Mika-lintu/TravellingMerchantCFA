using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    //Mika

    static Text coins;

    public static int amount;

    void Awake()
    {
        coins = GetComponent<Text>();
    }

    private void Start()
    {
        SetAmount(1000f);
    }

    public static void AddCoins(float num)
    {
        amount += (int)num;
        coins.text = "" + amount;
    }
    public static void RemoveCoins(float num)
    {
        amount -= (int)num;
        coins.text = "" + amount;
    }

    public static void SetAmount(float num)
    {
        amount = (int)num;
        coins.text = "" + amount;
    }
}
