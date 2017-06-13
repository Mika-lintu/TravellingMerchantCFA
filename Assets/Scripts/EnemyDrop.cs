using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    float coinDrop;
    public float minCoinDrop;
    public float maxCoinDrop;
    
    public void CoinDrop()
    {
        float dropChance = Random.Range(0f, 1f);
        if (dropChance > 0.3f) {
            coinDrop = Random.Range(minCoinDrop, maxCoinDrop);
            Debug.Log(coinDrop);
            Coins.AddCoins(coinDrop);
        }
    }
}
