using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    public int coins;
    public void AddCoins(int value)
    {
        coins += value; Debug.Log($"TOTAL COINS {coins}");
    }
}
