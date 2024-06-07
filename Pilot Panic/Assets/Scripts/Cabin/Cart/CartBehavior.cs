using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartBehavior : InteractableBehavior
{
    [SerializeField] int cartCapacity;
    [SerializeField] PlayerMovement player;
    [SerializeField] HingeJoint2D joint;

    [SerializeField]
    Sprite[] m_CartSprites;
    [SerializeField]
    SpriteRenderer CartSR;

    [SerializeField] CartGiveSnackZone giveSnack;

    public bool m_IsHeld { get; private set; }

    private void Awake()
    {
        m_IsHeld = false;
        //joint = GetComponent<HingeJoint2D>();
        joint.enabled = false;
        giveSnack.SetCapacity(cartCapacity);
        CartSR.sprite = m_CartSprites[0];
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

    public override void InRange(bool isInRange)
    {
        Debug.Log(isInRange);
        CartSR.sprite = m_CartSprites[isInRange ? 1 : 0];
        base.InRange(isInRange);
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
