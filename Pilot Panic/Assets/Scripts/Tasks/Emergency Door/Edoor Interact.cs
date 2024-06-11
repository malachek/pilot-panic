using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : InteractableBehavior
{

    public override PickupableBehavior GetInteract()
    {
        Debug.Log("Opened door...");
        SceneManager.LoadScene("FallLose");
        return null;
    }

}
