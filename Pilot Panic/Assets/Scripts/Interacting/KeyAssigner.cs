using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyAssigner : MonoBehaviour
{
    InteractableBehavior[] Interactables;
    //[SerializeField] string[] Keys;
    [SerializeField] string KeyString;
    public List<string> UsedKeys { get; private set; } 
    List<string> KeysList;
    void Awake()
    {
        KeysList = KeyString.ToCharArray().Select(i => i.ToString()).ToList();
        foreach(string key in KeyString.Split(""))
        {
            Debug.Log(key);
        }
        UsedKeys = new List<string>();
        Interactables = FindObjectsOfType<InteractableBehavior>();
        AssignKeys();
    }

    private void AssignKeys()
    {
        foreach (InteractableBehavior interactable in Interactables)
        {
            if (KeysList.Count == 0)
            {
                KeysList = KeyString.ToCharArray().Select(i => i.ToString()).ToList();
            }
            int keyIndex = Random.Range(0, KeysList.Count);

            if (interactable is PassengerBehavior)
            {
                ((PassengerBehavior)interactable).MyHappiness = 1f;
            }
            interactable.AssignKey(KeysList[keyIndex]);
            Debug.Log($"{interactable.name} is {KeysList[keyIndex]}");
            UsedKeys.Add(KeysList[keyIndex]);

            KeysList.RemoveAt(keyIndex);
        }
    }
}
