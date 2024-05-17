using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBehavior : InteractableBehavior
{
    [SerializeField] PlayerMovement player;
    private HingeJoint2D joint;

    public bool m_IsHeld { get; private set; }

    private void Awake()
    {
        m_IsHeld = false;
        joint = GetComponent<HingeJoint2D>();
        joint.enabled = false;
    }

    public override void Interact()
    {
        m_IsHeld = !m_IsHeld; 
        //gameObject.transform.SetParent(player.gameObject.transform, m_IsHeld);
        joint.enabled = m_IsHeld;
        Debug.Log($"IsHeld = {m_IsHeld}");
    }
}
