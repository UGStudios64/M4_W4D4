using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} make {damage} damages to {other.gameObject.name}");
        Life life = other.GetComponentInParent<Life>();
        life.TakeDamage(damage);

        //Destroy(gameObject);
    }
}
