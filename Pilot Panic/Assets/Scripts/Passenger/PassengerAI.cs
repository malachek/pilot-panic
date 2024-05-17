using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerAI : MonoBehaviour
{
    PassengerBehavior behavior;
    //NavMeshAgent agent;
    //Animator anim;
    PassengerState currentState;

    void Start()
    {
        //agent = this.GetComponent<NavMeshAgent>();
        //anim = this.GetComponent<Animator>();
        currentState = new Idle(this.gameObject);
        Debug.Log(gameObject);
    }

    void Update()
    {
        currentState = currentState.Process();   
    }

    public PassengerState State()
    {
        return currentState;
    }
}
