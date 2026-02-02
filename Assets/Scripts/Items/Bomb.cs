using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} take {damage} damages");
        Life life = other.GetComponentInParent<Life>();
        life.TakeDamage(damage);

        //Destroy(gameObject);
    }
}
