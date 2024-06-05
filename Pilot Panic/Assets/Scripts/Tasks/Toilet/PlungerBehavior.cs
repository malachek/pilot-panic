using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerBehavior : PickupableBehavior
{
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Toilet"))
        //{
        //    ToiletBehavior toilet = collision.gameObject.GetComponent<ToiletBehavior>();

        //    if (!toilet.IsAssignedTask) { return; }

        //    if (toilet.MyTask.name == "Plunge" && toilet.IsAcceptedTask) //if toilet wants to be plunged
        //    {
        //        toilet.CompleteTask(true);
        //        DestroyMe();
        //    }
        //}
    }
}