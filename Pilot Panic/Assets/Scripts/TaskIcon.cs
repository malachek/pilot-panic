using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIcon : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] SpriteRenderer m_TaskAlert;
    [SerializeField] Sprite m_ExclamationTaskSprite;
    [SerializeField] float TaskIconSize;

    [SerializeField] Gradient TaskGradient;

    Sprite m_TaskSprite;
    

    // Start is called before the first frame update
    void Awake()
    {
        m_TaskAlert.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (TaskIconSize <= .01f) { TaskIconSize = .5f; }
        m_TaskAlert.transform.localScale = new Vector3(TaskIconSize, TaskIconSize, 1);
    }

    public void AssignedTask(Task task, float askTime)
    {
        SetActiveSprite(m_ExclamationTaskSprite);

        m_TaskSprite = task.sprite;
        StartCoroutine(TimerGradient(askTime));
    }

    public void AcceptedTask(float waitTime)
    {
        SetActiveSprite(m_TaskSprite);

        StartCoroutine(TimerBlink(waitTime));
    }

    private void SetActiveSprite(Sprite sprite)
    {
        StopAllCoroutines();
        m_TaskAlert.color = Color.white;
        m_TaskAlert.transform.localScale = new Vector3(TaskIconSize, TaskIconSize, 1);

        m_TaskAlert.sprite = sprite;

        m_TaskAlert.gameObject.SetActive(true);
    }

    
    private IEnumerator TimerGradient(float maxTimerTime)
    {
        float timerTime = 0f;
        while (timerTime < maxTimerTime)
        {
            timerTime += Time.deltaTime;
            m_TaskAlert.color = TaskGradient.Evaluate(timerTime / maxTimerTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator TimerBlink(float maxTimerTime)
    {
        float timerTime = 0f;
        float blinkWaitTime = maxTimerTime * CalculateBlinkTime(0f);
        float currentBlinkTimer = 0f;

        while (timerTime < maxTimerTime)
        {
            timerTime += Time.deltaTime;
            blinkWaitTime -= Time.deltaTime;

            if (blinkWaitTime < 0f)
            {
                blinkWaitTime = maxTimerTime * CalculateBlinkTime(timerTime / maxTimerTime);
                Debug.Log("Blink wait for " + blinkWaitTime);
                currentBlinkTimer = .05f;
            }
            if (currentBlinkTimer > .01f)
            {
                m_TaskAlert.transform.localScale = new Vector3(TaskIconSize * 1.2f, TaskIconSize * 1.2f, 1f);
                currentBlinkTimer -= Time.deltaTime;
                if (currentBlinkTimer <= 0f)
                {
                    m_TaskAlert.transform.localScale = new Vector3(TaskIconSize, TaskIconSize, 1f);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private float CalculateBlinkTime(float percent)
    {
        return (percent - 1) * (percent - 1) / 3f;
        return (1 - percent) / 4f;
        return 2 / (percent + 1) + 1;
        return 1f - (percent * percent);
    }

    public void CompletedTask()
    {
        m_TaskSprite = null;
        Show(false);
    }

    public void Show(bool show)
    {
        m_TaskAlert.gameObject.SetActive(show);
    }
}
