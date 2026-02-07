using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"+{value}");
            CoinsHandler coinsHandler = other.GetComponentInParent<CoinsHandler>();
            coinsHandler.AddCoins(value);

            Destroy(gameObject);
        }
    }
}
