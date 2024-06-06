using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

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
    
    int m_SpriteIndex;

    public float MyHappiness = 1f;
    
    [Header("Tasks")]
    [SerializeField] SpriteRenderer m_TaskAlert;
    [SerializeField] Sprite m_ExclamationTaskSprite;
    [SerializeField] float TaskIconSize;
    public Task MyTask { get; private set; }
    float m_TaskPatienceTimer;

    public bool IsBumped { get; private set; }
    public bool IsAssignedTask { get; private set; }
    public bool IsAcceptedTask { get; private set; }


    private void Awake()
    {
        m_CharacterSprites = passengerStats.Sprites;
        m_SpriteIndex = 0;
        m_CharacterSpriteRenderer.sprite = m_CharacterSprites[m_SpriteIndex];
        m_TaskAlert.gameObject.SetActive(false);
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

        if (TaskIconSize <= .01f) { TaskIconSize = .5f; }
        m_TaskAlert.transform.localScale = new Vector3(TaskIconSize, TaskIconSize, 1);
    }

    public override PickupableBehavior GetInteract()
    {
        if (IsAssignedTask) { AcceptTask(); }
        return null;
    }

    //KEEP
    public float Weight()
    {
        if(MyHappiness <= .01f) { return 0; }//////////////////////////////////////////////
        return (4 / (3 * MyHappiness) + 1);
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

        m_TaskAlert.gameObject.SetActive(false);

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
    {for (int i = 2; i >= 0; i--)
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
        m_TaskAlert.sprite = m_ExclamationTaskSprite;
        m_TaskAlert.gameObject.SetActive(true);
    }

    private void AcceptTask()
    {
        IsAcceptedTask = true;
        m_TaskAlert.sprite = MyTask.sprite;
        Debug.Log($"{gameObject.name} accepted task: {MyTask.name} - {MyTask.description}");
        m_TaskAlert.gameObject.SetActive(true);
        m_TaskPatienceTimer = Random.Range(MyTask.minWaitTime, MyTask.maxWaitTime) * CalculatePatience();
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
}
