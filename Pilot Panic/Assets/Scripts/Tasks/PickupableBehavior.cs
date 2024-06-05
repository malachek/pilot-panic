using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableBehavior : MonoBehaviour
{
    [SerializeField] GameObject parent;
    PlayerMovement player;

    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    protected void SetPosition()
    {
        gameObject.transform.localPosition = player.m_VelocityVector / 5;
    }

    public void Drop()
    {
        DestroyMe();
    }

    protected void DestroyMe()
    {
        Destroy(parent);
    }
}
