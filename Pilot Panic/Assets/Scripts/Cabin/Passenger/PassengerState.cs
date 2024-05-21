using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassengerState
{
    public enum STATE
    {
        IDLE, ASK, WAIT, WANDER
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE stateName;
    protected EVENT stage;
    protected GameObject passenger;
    protected PassengerBehavior behavior;
    //protected Animator anim;
    protected PassengerState nextState;
    //protected NavMeshAgent agent;

    public PassengerState(GameObject _passenger) //NavMeshAgent _agent, Animator _anim
    {
        stage = EVENT.ENTER;
        //agent = _agent;
        passenger = _passenger;
        behavior = passenger.GetComponent<PassengerBehavior>();
        //anim = _anim;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public PassengerState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            //Debug.Log($"Going to {nextState} now");
            return nextState;
        }

        return this;
    }
}

public class Idle : PassengerState
{
    public Idle(GameObject _passenger) : base(_passenger) 
    {
        stateName = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (behavior.IsAssignedTask)
        {
            //Debug.Log("queueing ask");
            nextState = new Ask(passenger);
            stage = EVENT.EXIT;
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Ask : PassengerState
{
    readonly Task AssignedTask;
    public float TaskPatienceTimer { get; private set; }


    public Ask(GameObject _passenger) : base(_passenger)
    {
        stateName = STATE.ASK;
        AssignedTask = behavior.MyTask;
        TaskPatienceTimer = Random.Range(AssignedTask.minAskTime, AssignedTask.maxAskTime) * behavior.CalculatePatience();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (behavior.IsAcceptedTask)
        {
            //Debug.Log("queueing wait");
            nextState = new Wait(passenger);
            stage = EVENT.EXIT;
        }
            
        TaskPatienceTimer -= Time.deltaTime;
        if (TaskPatienceTimer >= 0) { return; }

        //if timer expired
        behavior.CompleteTask(false);
        //Debug.Log("queueing idle");
        nextState = new Idle(passenger);
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
public class Wait : PassengerState
{
    readonly Task AssignedTask;
    public float TaskPatienceTimer { get; private set; }


    public Wait(GameObject _passenger) : base(_passenger)
    {
        stateName = STATE.WAIT;
        AssignedTask = behavior.MyTask;
        TaskPatienceTimer = Random.Range(AssignedTask.minWaitTime, AssignedTask.maxWaitTime) * behavior.CalculatePatience();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (!behavior.IsAssignedTask) //once they don't have a task, it's completed
        {
            //behavior.CompleteTask(true);
            //Debug.Log("queing idle");
            nextState = new Idle(passenger);
            stage = EVENT.EXIT;
        }

        TaskPatienceTimer -= Time.deltaTime;
        if (TaskPatienceTimer >= 0) { return; }

        //if timer expired
        behavior.CompleteTask(false);
        //Debug.Log("queueing idle");
        nextState = new Idle(passenger);
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }
}

/*public class Wander : PassengerState
{
    readonly Task AssignedTask;
    
    public Wander(GameObject _passenger) : base(_passenger)
    {
        stateName = STATE.WANDER;
        AssignedTask = behavior.MyTask;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}*/
