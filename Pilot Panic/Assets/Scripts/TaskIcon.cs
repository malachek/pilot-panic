using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIcon : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] SpriteRenderer m_TaskAlert;
    [SerializeField] Sprite m_ExclamationTaskSprite;
    [SerializeField] float TaskIconSize;

    float m_MaxAskTime;
    float m_TimerTime;
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
        m_TaskSprite = task.sprite;
        m_TaskAlert.sprite = m_ExclamationTaskSprite;
        m_TaskAlert.gameObject.SetActive(true);
    }

    public void AcceptedTask(float waitTime)
    {
        m_TaskAlert.sprite = m_TaskSprite;
        m_TaskAlert.gameObject.SetActive(true);
        
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
