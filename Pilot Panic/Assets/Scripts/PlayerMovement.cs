using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float m_MaxSpeed;
    [SerializeField] float m_Acceleration;
    [SerializeField] float m_Decelleration;
    
    Vector2 m_InputVector;
    Vector2 m_VelocityVector;

    [SerializeField] SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_InputVector = new Vector2();
        m_VelocityVector = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        m_InputVector.x = Input.GetAxis("Horizontal");
        m_InputVector.y = Input.GetAxis("Vertical");

        //m_InputVector *= m_MovementSpeed;

        //rb.velocity = m_MovementVector;
    }

    private void FixedUpdate()
    {
        HandleHorizontal();
        ApplyMovement();
    }

    private void HandleHorizontal()
    {
        if (m_InputVector.x ==0)
        {
            m_VelocityVector.x = Mathf.MoveTowards(m_VelocityVector.x, 0, m_Decelleration * Time.fixedDeltaTime);
        }
        else
        {
            m_VelocityVector.x = Mathf.MoveTowards(m_VelocityVector.x, m_InputVector.x * m_MaxSpeed, m_Acceleration * Time.fixedDeltaTime);
        }

        if (m_InputVector.y == 0)
        {
            m_VelocityVector.y = Mathf.MoveTowards(m_VelocityVector.y, 0, m_Decelleration * Time.fixedDeltaTime);
        }
        else
        {
            m_VelocityVector.y = Mathf.MoveTowards(m_VelocityVector.y, m_InputVector.y * m_MaxSpeed, m_Acceleration * Time.fixedDeltaTime);
        }

        
        if (m_VelocityVector.x < 0)
        {
            m_SpriteRenderer.flipX = false;
        }
        else if (m_VelocityVector.x > 0)
        {
            m_SpriteRenderer.flipX = true;
        }
    }

    private void ApplyMovement() => rb.velocity = m_VelocityVector;
}
