using UnityEngine;
using UnityEngine.Events;

public class CoinsHandler : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] private UnityEvent<int> OnCoinsChanged;

    #region// GET -----------------------------------------------------------------------------------------------------------------------
    public int GetCoins() => coins;
    #endregion


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Start()
    { OnCoinsChanged.Invoke(coins); }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void AddCoins(int value)
    {
        coins += value; Debug.Log($"TOTAL COINS {coins}");
        OnCoinsChanged.Invoke(coins);
    }
}