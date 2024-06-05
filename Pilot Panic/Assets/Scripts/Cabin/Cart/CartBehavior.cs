using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartBehavior : InteractableBehavior
{
    [SerializeField] int cartCapacity;
    [SerializeField] PlayerMovement player;
    private HingeJoint2D joint;

    [SerializeField] CartGiveSnackZone giveSnack;

    public bool m_IsHeld { get; private set; }

    private void Awake()
    {
        m_IsHeld = false;
        joint = GetComponent<HingeJoint2D>();
        joint.enabled = false;
        giveSnack.SetCapacity(cartCapacity);
    }

    public override PickupableBehavior GetInteract()
    {
        m_IsHeld = !m_IsHeld;
        //gameObject.transform.SetParent(player.gameObject.transform, m_IsHeld);
        joint.enabled = m_IsHeld;
        Debug.Log($"IsHeld = {m_IsHeld}");
        return null;
        //return this.gameObject;
    }

    public void PickUp(SnackBehavior snack)
    {
        giveSnack.PickUp(snack.name);
    }

    /*public override void AssignKey(string key)
    {
        Key = key;
    }
    public override void InRange(bool isInRange)
    {
        IsInRange = isInRange;
    }*/

}
