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
    public Vector2 m_VelocityVector;

    [SerializeField] InteractDetector m_InteractDetector;
    private InteractableBehavior m_SelectedInteractable;
    public InteractableBehavior myInteractable;
    public bool m_IsInteracting { get; private set; }
    public InteractableBehavior m_InteractingInteractable { get; private set; }

    [SerializeField] SpriteRenderer m_SpriteRenderer;

    //[SerializeField] float InteractButtonHoldTime;
    //float m_KeyHoldTime;
    List<string> Keys;

    bool hasInteracted = false;

    void Awake()
    {
        m_InputVector = new Vector2();
        m_VelocityVector = new Vector2();
        m_IsInteracting = false;
        m_InteractingInteractable = null;
    }

    private void Start()
    {
        Keys = FindObjectOfType<KeyAssigner>().UsedKeys;
    }

    void Update()
    {
        m_InputVector.x = Input.GetAxis("Horizontal");
        m_InputVector.y = Input.GetAxis("Vertical");

        //DetermineKeyHeld();
        AnyKeyDown(Keys);
    }

    private bool AnyKeyDown(IEnumerable<string> keys)
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log(key);
                InteractWith(key);
                return true;
            }
        }
        return false;
    }

    private void InteractWith(string key)
    {
        InteractableBehavior Interactable = m_InteractDetector.GetInteractable(key);
        if (Interactable == null) return;

        Interactable.Interact();
        myInteractable = Interactable;
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

/*private void DetermineKeyHeld()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        hasInteracted = false;
        m_KeyHoldTime = 0f;
    }

    if (Input.GetKey(KeyCode.E))
    {
        m_KeyHoldTime += Time.deltaTime;
        if (m_KeyHoldTime >= InteractButtonHoldTime && !hasInteracted)
        {
            hasInteracted = true;
            //Debug.Log($"Key hold time: {m_KeyHoldTime} | Hold time threshhold {InteractButtonHoldTime}");
            InteractInteractable();
        }
    }
    //FOR E PRESS
    if (Input.GetKeyUp(KeyCode.E))
    {
        if (m_KeyHoldTime < InteractButtonHoldTime)
        {
            m_InteractDetector.CycleInteractables();
        }
    }
}*/

/*private void InteractInteractable()
{
    if (m_IsInteracting) // for cart - need to change later to be more inclusive
    {
        m_InteractingInteractable.Interact();
        m_IsInteracting = false;
        m_InteractingInteractable = null;
        return;
    }

    //m_SelectedInteractable = m_InteractDetector.GetInteractable();
    if(m_SelectedInteractable == null) { return; }

    Debug.Log($"Interacted with {m_SelectedInteractable.gameObject.name}");
    m_SelectedInteractable.Interact();

    if(m_SelectedInteractable.gameObject.CompareTag("Cart"))
    {
        m_IsInteracting = true;
        m_InteractingInteractable = m_SelectedInteractable;
    }
}*/