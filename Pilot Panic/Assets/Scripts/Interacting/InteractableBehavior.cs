using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    public TextMeshPro m_KeyText;
    public string Key { get; protected set; }
    public bool IsInRange { get; protected set; }

    
    public virtual void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}. Nothing happened.");
    }

    private void Awake()
    {
        m_KeyText.gameObject.SetActive(false);
    }

    public void AssignKey(string key)
    {
        Key = key;
        m_KeyText.text = $"[{key}]";
        m_KeyText.gameObject.SetActive(false);
    }
    public void InRange(bool isInRange)
    {
        //Debug.Log(gameObject.name + isInRange);
        IsInRange = isInRange;
        m_KeyText.gameObject.SetActive(isInRange);
    }
}
