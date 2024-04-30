using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PassengerTask : MonoBehaviour
{
    [SerializeField] float m_MaxTaskAssignTime;
    float m_TaskAssignTimer;

    bool m_WaitingOnTaskCompletion = false;
    [SerializeField] float m_TaskPatience;
    float m_TaskPatienceTimer;

    [SerializeField] float m_HappinessLoss;
    [SerializeField] float m_HappinessGain;

    [SerializeField] SpriteRenderer m_TaskAlert;
    
    // Start is called before the first frame update
    void Start()
    {
        m_TaskAssignTimer = m_MaxTaskAssignTime;
        m_TaskAlert.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_WaitingOnTaskCompletion)
        {
            if (m_TaskPatienceTimer > 0)
            {
                m_TaskPatienceTimer -= Time.fixedDeltaTime;
            }
            else
            {
                Debug.Log(":(");
                HappinessManager.ChangeHappiness(-m_HappinessLoss);
                m_TaskAlert.gameObject.SetActive(false);
                m_TaskAssignTimer = m_MaxTaskAssignTime;
                m_WaitingOnTaskCompletion = false;
            }
        }
        else
        {
            if (m_TaskAssignTimer > 0)
            {
                m_TaskAssignTimer -= Time.fixedDeltaTime;
            }
            else
            {
                Debug.Log("TASK!");
                HappinessManager.ChangeHappiness(m_HappinessGain);
                m_TaskAlert.gameObject.SetActive(true);
                m_TaskPatienceTimer = m_TaskPatience;
                m_WaitingOnTaskCompletion = true;
            }
        }
        
    }
}
