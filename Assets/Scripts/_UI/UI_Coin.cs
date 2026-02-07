using TMPro;
using UnityEngine;

public class UI_Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;
    [SerializeField] private ActiveDoor connectedDoor;
    private int needCoins;

    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    {
        if (!connectedDoor) Debug.LogWarning($"Missing Connected Door");
        if (!coinCounter) Debug.LogWarning($"Missing Coin Counter");

        needCoins = connectedDoor.GetNeedCoin();
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void UpdateCoinCounter(int coins)
    { coinCounter.text = coins + "/" + needCoins; }
}