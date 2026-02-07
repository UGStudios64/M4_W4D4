using UnityEngine;
using UnityEngine.Events;

public class TimeExtra : MonoBehaviour
{
    [SerializeField] private int time;
    [SerializeField] private bool InMinutes;
    [SerializeField] private UnityEvent<float, bool> OnTimeExtra;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTimeExtra.Invoke(time, InMinutes);
            Destroy(gameObject);
        }
    }
}