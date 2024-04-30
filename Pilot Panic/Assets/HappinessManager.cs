using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    static TextMeshProUGUI HappinessText;
    [SerializeField] float m_MaxHappiness;
    public static float Happiness { get; private set; }
    
    void Awake()
    {
        Happiness = m_MaxHappiness;
        HappinessText = FindObjectOfType<TextMeshProUGUI>();
        HappinessText.text = $"Happiness: {Happiness}";
    }

    

    public static void ChangeHappiness(float change)
    {
        Happiness += change;
        if(Happiness > 100) { Happiness = 100; }
        if (Happiness <= 0)
        {
            Debug.Log("GAME OVER");
            return;
        }
        HappinessText.text = $"Happiness: {Happiness}";
    }
}
