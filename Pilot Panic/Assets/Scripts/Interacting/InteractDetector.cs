using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    public Dictionary<string, InteractableBehavior> NearbyInteractables { get; private set; }
    
    private void Awake()
    {
        NearbyInteractables = new Dictionary<string, InteractableBehavior>();
    }

    public InteractableBehavior GetInteractable(string key)
    {
        try { return NearbyInteractables[key];}
        catch (KeyNotFoundException) { return null; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<InteractableBehavior>(out InteractableBehavior interactable))
        {
            NearbyInteractables.Add(interactable.Key, interactable);
            interactable.InRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<InteractableBehavior>(out InteractableBehavior interactable))
        {
            interactable.InRange(false);
            NearbyInteractables.Remove(interactable.Key);
        }
    }

}
