using System.Collections;
using System.Collections.Generic;
// using UnityEditor.UI;
using UnityEngine;

public class SnackBehavior : PickupableBehavior
{
    //[SerializeField] GameObject parent;
    //PlayerMovement player;
    bool inCart;

    protected override void Start()
    {
        inCart = false;
        base.Start();
    }

    private void Update()
    {
        if (inCart) { return; }
        base.SetPosition(); ;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Passenger"))
        {
            PassengerBehavior passenger = collision.gameObject.GetComponent<PassengerBehavior>();
            if(!passenger.IsAssignedTask) { return; }

            if (passenger.MyTask.name == "Chips" && passenger.IsAcceptedTask)
            {
                passenger.CompleteTask(true);
                DestroyMe();
            }
        }
        if(collision.CompareTag("CartSnackCollect"))
        {
            collision.transform.parent.GetComponent<CartBehavior>().PickUp(this);
            DestroyMe();
        }
    }
}
