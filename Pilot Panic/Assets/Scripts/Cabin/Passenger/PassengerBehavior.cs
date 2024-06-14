using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using FMODUnity;

// using UnityEditor.VersionControl;
using UnityEngine;

public class PassengerBehavior : InteractableBehavior
{
    [Header("Basic Settings")]
    [SerializeField] PassengerScriptableObject passengerStats;
    Sprite[] m_CharacterSprites;
    float[] m_MoodMilestones;
    float m_HappinessGainMultipler;
    float m_HappinessLossMultiplier;
    float m_PatienceAtZero;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer m_CharacterSpriteRenderer;

    [SerializeField] TaskIcon taskIconManager;
    public Task MyTask { get; private set; }


    int m_SpriteIndex;

    public float MyHappiness = 1f;

    public bool IsBumped { get; private set; }
    public bool IsAssignedTask { get; private set; }
    public bool IsAcceptedTask { get; private set; }



    private void Awake()
    {
        m_CharacterSprites = passengerStats.Sprites;
        m_SpriteIndex = 0;
        m_CharacterSpriteRenderer.sprite = m_CharacterSprites[m_SpriteIndex];
    }

    private void Start()
    {
        IsBumped = false;
        IsAssignedTask = false;
        IsAcceptedTask = false;
        m_MoodMilestones = passengerStats.MoodMileStones;
        m_HappinessGainMultipler = passengerStats.HappinessGainMultiplier;
        m_HappinessLossMultiplier = passengerStats.HappinessLossMultiplier;
        m_PatienceAtZero = passengerStats.PatienceAtZero;
    }

    public override PickupableBehavior GetInteract()
    {
        if (IsAssignedTask) AcceptTask();
        return null;
    }

    //KEEP
    public float Weight()
    {
        //if(MyHappiness <= .01f) { return 0; }//////////////////////////////////////////////
        return (4 / (3 * MyHappiness + 1));
    }


    public void CompleteTask(bool success)
    {
        if (success)
        {
            Debug.Log(":)");
            ChangeHappiness(MyTask.happinessGain * m_HappinessGainMultipler);
        }
        else
        {
            Debug.Log(":(");
            ChangeHappiness(-MyTask.happinessLoss * m_HappinessLossMultiplier);
        }

        taskIconManager.CompletedTask();

        GameObject.FindObjectOfType<TaskManager>().CompletedTask(MyTask, this, success);
        MyTask = null;
        IsAssignedTask = false;
        IsAcceptedTask = false;
    }

    void ChangeHappiness(float happinessChange)
    {
        float NewHappiness = Mathf.Clamp(MyHappiness + happinessChange, 0f, 1f);
        Debug.Log($"{gameObject.name}'s Happiness = {NewHappiness * 100}% ({MyHappiness} + {happinessChange})");
        CheckSprites(NewHappiness);
        MyHappiness = NewHappiness;
        HappinessManager.onHappinessChange(happinessChange);
    }

    private void CheckSprites(float NewHappiness)
    {
        for (int i = 2; i >= 0; i--)
        {
            if (NewHappiness < m_MoodMilestones[i])
            {
                if (m_SpriteIndex == i) return;
                m_SpriteIndex = i;
                m_CharacterSpriteRenderer.sprite = m_CharacterSprites[m_SpriteIndex];
                return;
            }
        } 
    }

    public void AssignTask(Task task)
    {
        
        MyTask = task;
        IsAssignedTask = true;
        Debug.Log($"{gameObject.name} assigned task: {MyTask.name} - {MyTask.description}");

        //taskIconManager.AssignedTask(task, m_TaskPatienceTimer);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Grunts", GetComponent<Transform>().position);

        if (IsInRange) InRange(true);
       
    }
   

    private void AcceptTask()
    {
        IsAcceptedTask = true;

        Debug.Log($"{gameObject.name} accepted task: {MyTask.name} - {MyTask.description}");

        //taskIconManager.AcceptedTask(m_TaskPatienceTimer);
         FMODUnity.RuntimeManager.PlayOneShot("event:/TaskDONE", GetComponent<Transform>().position);
        if (IsInRange) InRange(true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))// && (MyState == PassengerState.Idle))
        {
            Bumped();
        }
    }

    private void Bumped()
    {
        if (!IsBumped)
            Debug.Log("OW!");
        ChangeHappiness(-MyHappiness * (1 - MyHappiness));
        StartCoroutine(BumpedCD());
    }

    private IEnumerator BumpedCD()
    {
        IsBumped = true;
        yield return new WaitForSeconds(CalculatePatience());
        IsBumped = false;
    }

    //KEEP
    public float CalculatePatience()
    {
        return (1 - m_PatienceAtZero) * MyHappiness + m_PatienceAtZero;
    }

    public override void InRange(bool isInRange)
    {
        //Debug.Log(gameObject.name + isInRange);
        IsInRange = isInRange;
        m_KeyText.gameObject.SetActive((IsAssignedTask || IsAcceptedTask) && isInRange);
    }
}
