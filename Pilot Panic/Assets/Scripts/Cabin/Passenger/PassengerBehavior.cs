using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

// using UnityEditor.VersionControl;
using UnityEngine;

public class PassengerBehavior : InteractableBehavior
{
    const float PATIENCE_AT_ZERO = .5f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Sprite m_CharacterSprite;
    [SerializeField] SpriteRenderer m_CharacterSpriteRenderer;
    //[SerializeField] TextMeshPro m_KeyText;

    public Task MyTask { get; private set; }

    float m_TaskPatienceTimer;

    public float MyHappiness = 1f;

    [SerializeField] SpriteRenderer m_TaskAlert;
    [SerializeField] Sprite m_ExclamationTaskSprite;
    [SerializeField] float TaskIconSize;

    public bool IsBumped { get; private set; }
    public bool IsAssignedTask { get; private set; }
    public bool IsAcceptedTask { get; private set; }


    private void Awake()
    {
        m_CharacterSpriteRenderer.sprite = m_CharacterSprite;
        m_TaskAlert.gameObject.SetActive(false);
        //m_KeyText.gameObject.SetActive(false);

        //MyState = PassengerState.Idle;
    }

    private void Start()
    {
        //myInteractType = InteractType.Button;
        IsBumped = false;
        IsAssignedTask = false;
        IsAcceptedTask = false;

        if (TaskIconSize <= .01f) { TaskIconSize = .5f; }
        m_TaskAlert.transform.localScale = new Vector3(TaskIconSize, TaskIconSize, 1);
    }

    void FixedUpdate()
    {
        /*if (MyState is not PassengerState.Idle)
        {
            if (m_TaskPatienceTimer > 0)
            {
                m_TaskPatienceTimer -= Time.fixedDeltaTime;
            }
            else
            {
                CompleteTask(false);
            }
        }*/
    }

    public override PickupableBehavior GetInteract()
    {
        if (IsAssignedTask) { AcceptTask(); }
        //Debug.Log($"Interacted () with {gameObject.name}");
        //{ myInteractType}
        //Debug.Log($"My task is {MyTask}");
        return null;
    }

    //KEEP
    public float Weight()
    {
        //returns 0 if state is not Idle
        if(MyHappiness <= .01f) { return 0; }//////////////////////////////////////////////
        return (4 / (3 * MyHappiness) + 1); //* (MyState is PassengerState.Idle ? 1 : 0);
    }


    public void CompleteTask(bool success)
    {
        if (success)
        {
            Debug.Log(":)");
            ChangeHappiness(MyTask.happinessGain);
        }
        else
        {
            Debug.Log(":(");
            ChangeHappiness(-MyTask.happinessLoss);
        }

        m_TaskAlert.gameObject.SetActive(false);

        GameObject.FindObjectOfType<TaskManager>().CompletedTask(MyTask, this, success);
        MyTask = null;
        IsAssignedTask = false;
        IsAcceptedTask = false;
        //MyState = PassengerState.Idle;
    }

    void ChangeHappiness(float happinessChange)
    {
        float NewHappiness = Mathf.Clamp(MyHappiness + happinessChange, 0f, 1f);
        Debug.Log($"{gameObject.name}'s Happiness = {NewHappiness * 100}% ({MyHappiness} + {happinessChange})");
        MyHappiness = NewHappiness;
        HappinessManager.onHappinessChange(happinessChange);
    }

    public void AssignTask(Task task)
    {
        //MyState = PassengerState.Asking;
        MyTask = task;
        IsAssignedTask = true;
        Debug.Log($"{gameObject.name} assigned task: {MyTask.name} - {MyTask.description}");
        m_TaskAlert.sprite = m_ExclamationTaskSprite;
        m_TaskAlert.gameObject.SetActive(true);
        //m_TaskPatienceTimer = Random.Range(MyTask.minAskTime, MyTask.maxAskTime) * CalculatePatience();
        //MyState = PassengerState.Asking;
    }

    private void AcceptTask()
    {
        //MyState = PassengerState.Waiting;
        IsAcceptedTask = true;
        m_TaskAlert.sprite = MyTask.sprite;
        Debug.Log($"{gameObject.name} accepted task: {MyTask.name} - {MyTask.description}");
        m_TaskAlert.gameObject.SetActive(true);
        m_TaskPatienceTimer = Random.Range(MyTask.minWaitTime, MyTask.maxWaitTime) * CalculatePatience();
        //MyState = PassengerState.Waiting;
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
        return (1 - PATIENCE_AT_ZERO) * MyHappiness + PATIENCE_AT_ZERO;
    }

    /*public override void AssignKey(string key)
    {
        Key = key;
        m_KeyText.text = $"[{key}]";
    }
    public override void InRange(bool isInRange)
    {
        //Debug.Log(gameObject.name + isInRange);
        IsInRange = isInRange;
        m_KeyText.gameObject.SetActive(isInRange);
    }*/

}
