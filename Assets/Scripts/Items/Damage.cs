using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} make {damage} damages to {other.gameObject.name}");
            Life life = other.GetComponentInParent<Life>();
            life.TakeDamage(damage);
        }
    }
}