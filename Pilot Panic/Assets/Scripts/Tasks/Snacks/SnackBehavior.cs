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
        //Vector3 newRotation = new Vector3(0, Random.Range(-45f, 45f), 0);
        //this.transform.eulerAngles = newRotation;
        inCart = false;
        base.Start();
        //player = FindAnyObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (inCart) { return; }
        base.SetPosition(); ;
        //gameObject.transform.localPosition = player.m_VelocityVector / 5;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Passenger"))
        {
            PassengerBehavior passenger = collision.gameObject.GetComponent<PassengerBehavior>();
            //if(passenger.MyState != PassengerBehavior.PassengerState.Idle)
            if(!passenger.IsAssignedTask) { return; }

            if (passenger.MyTask.name == "Chips" && passenger.IsAcceptedTask)
            {
                passenger.CompleteTask(true);
                DestroyMe();
            }
        }
        if(collision.CompareTag("CartSnackCollect"))
        {
            /*inCart = true;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            parent.transform.SetParent(collision.gameObject.transform);*/
            collision.transform.parent.GetComponent<CartBehavior>().PickUp(this);
            DestroyMe();
        }
    }
}
