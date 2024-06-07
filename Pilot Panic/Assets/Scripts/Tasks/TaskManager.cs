using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] Task[] tasks;
    private Task m_NextTask;

    [Header("Passengers")]
    public PassengerBehavior[] passengers;
    private float m_TotalTaskWeight;

    [Header("Assigning")]
    [SerializeField, Range(0, 1f)] float assignmentTimeVariance;
    [SerializeField] float maxAssignmentTime;
    private float m_AssignmentTimer;
    public int AssignedTasks { get; private set; } = 0;


    //====================================================//

    /*static TaskManager _instance;


    private void Awake()
    {
        Debug.Log("Awake method called in " + gameObject.name);
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }*/

    

    void Start()
    {
        ResetAssignmentTimer(5f, 0f); //Initial set of timer
        passengers = FindObjectsOfType<PassengerBehavior>();

        m_NextTask = WeighTasks();
    }


    void FixedUpdate()
    {
        m_AssignmentTimer -= Time.fixedDeltaTime;
        if (m_AssignmentTimer <= 0f)
        {
            if (AssignTask())
            {
                AssignedTasks += 1;
                m_NextTask = WeighTasks();
            }
        }
    }

    
    public void CompletedTask(Task task, PassengerBehavior passenger, bool success)
    {
        AssignedTasks--;
        Debug.Log($"TASKM: Completed task ({task.name}) for {passenger.name}. success = {success}");
        task.Complete(passenger, success);
        return;
    }

    private bool AssignTask()
    {
        if (AssignedTasks >= 5)
        {
            return false;
        }

        ResetAssignmentTimer();

        float totalChance = 0f;
        foreach (PassengerBehavior passenger in passengers)
        {
            if (passenger.MyTask is not null) continue;
            totalChance += passenger.Weight();
        }
        float rand = UnityEngine.Random.Range(0f, totalChance);

        float cumulativeChance = 0f;
        foreach (PassengerBehavior passenger in passengers)
        {
            if (passenger.MyTask is not null) continue;
            cumulativeChance += passenger.Weight();
            if (rand <= cumulativeChance)
            {
                passenger.AssignTask(m_NextTask);
                m_NextTask.Assign(passenger);
                return true;
            }
        }
        return false;
    }

    private Task WeighTasks()
    {
        float totalChange = 0f;
        foreach (Task task in tasks)
        {
            totalChange += task.weight;
        }
        float rand = UnityEngine.Random.Range(0f, totalChange);

        float cumulativeChance = 0f;
        foreach (Task task in tasks)
        {
            cumulativeChance += task.weight;

            if(rand <= cumulativeChance)
            {
                return task;
            }
        }
        return null;
    }


    private void ResetAssignmentTimer()
    {
        ResetAssignmentTimer(maxAssignmentTime, assignmentTimeVariance);
    }
    private void ResetAssignmentTimer(float setTime, float variance)
    {
        m_AssignmentTimer = setTime * (1 - UnityEngine.Random.Range(-variance, variance));
        Debug.Log("resting timer to " +  (setTime * (1 - UnityEngine.Random.Range(-variance, variance))));
    }

}


[Serializable]
public class Task
{
    public string name;
    public string description;
    public Sprite sprite;
    [Space]
    [Tooltip("Between 0-1f")]
    public float happinessGain;
    [Tooltip("Between 0-1f")]
    public float happinessLoss;
    [Space]
    public int maxAssignees;
    public int assigneeCount = 0;
    public List<PassengerBehavior> assignedPassengers = new List<PassengerBehavior>();

    [Space]
    public float weight;
    [Space]
    public float minAskTime;        //how long passenger will wait for pilot to approach them
    public float maxAskTime;
    [Space]
    public float minWaitTime;       //how long passenger will wait for task to be completed
    public float maxWaitTime;       

    public void Assign(PassengerBehavior passenger)
    {
        assignedPassengers.Add(passenger);
        assigneeCount++;
    }

    public void Complete(PassengerBehavior passenger, bool success)
    {
        assignedPassengers.Remove(passenger);
        assigneeCount--;
    }
}