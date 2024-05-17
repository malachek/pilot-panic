using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackSpawner : InteractableBehavior
{
    [SerializeField] GameObject ChipsPrefab;

    private PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }



    public override void Interact()
    {
        var spawned = Instantiate(ChipsPrefab, player.transform);
        spawned.transform.SetParent(player.transform);
    }
}
