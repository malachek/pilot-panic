using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoorInteract : InteractableBehavior
{

    [SerializeField] GameObject EDoorPrefab;

    private PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public override PickupableBehavior GetInteract()
    {
        var spawned = Instantiate(EDoorPrefab, player.transform);
        spawned.transform.SetParent(player.transform);
        return null;
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
