using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] int heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Life life = other.GetComponentInParent<Life>();
            life.TakeHeal(heal);
            Debug.Log($"you got {heal}");

            Destroy(gameObject);
        }   
    }
}
