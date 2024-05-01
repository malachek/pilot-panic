using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    
    Vector3 m_MovementVector;
    [SerializeField] float m_MovementSpeed;

    [SerializeField] SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_MovementVector = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        m_MovementVector.x = Input.GetAxis("Horizontal");
        m_MovementVector.y = Input.GetAxis("Vertical");

        m_MovementVector *= m_MovementSpeed;

        rb.velocity = m_MovementVector;
    }

    private void FixedUpdate()
    {
        if (m_MovementVector.x < 0)
        {
            m_SpriteRenderer.flipX = false;
        }
        else if (m_MovementVector.x > 0)
        {
            m_SpriteRenderer.flipX = true;
        }
    }
}
