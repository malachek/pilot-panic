using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBehavior : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PassengerBehavior passengerBehavior;


    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
        }

    }
}
