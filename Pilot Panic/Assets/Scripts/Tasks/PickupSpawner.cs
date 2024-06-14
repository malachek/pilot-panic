using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : InteractableBehavior
{
    [SerializeField] GameObject SpawnablePrefab;

    private PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public override PickupableBehavior GetInteract()
    {
        var spawned = Instantiate(SpawnablePrefab, player.transform);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Chip Bag", GetComponent<Transform>().position);
        spawned.transform.SetParent(player.transform);
        return spawned.GetComponentInChildren<PickupableBehavior>();
    }
}
