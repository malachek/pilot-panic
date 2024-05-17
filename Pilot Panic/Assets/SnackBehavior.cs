using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackBehavior : MonoBehaviour
{
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Passenger"))
        {
            PassengerTask passenger = collision.gameObject.GetComponent<PassengerTask>();
            Debug.Log(passenger.MyTask.name);
            if (passenger.MyTask.name == "Chips")
            {
                passenger.CompleteTask(true);
                Destroy(gameObject);
            }
        }
    }
}
