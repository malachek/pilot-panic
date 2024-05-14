using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    static HappinessTextManager HappinessText;
    [SerializeField] float m_MaxHappiness;
    public static float Happiness { get; private set; }

    private static float CurrentTotalHappiness;
    private static float MaxTotalHappiness;

    public delegate void OnHappinessChange(float change);
    public static OnHappinessChange onHappinessChange;

    static HappinessManager _instance;
    void Awake()
    {
        Debug.Log("Awake method called in " + gameObject.name);
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
        NewGame();
    }

    public static void NewGame()
    {
        Happiness = _instance.m_MaxHappiness;
        HappinessText = GameObject.FindObjectOfType<HappinessTextManager>();
        MaxTotalHappiness = CountPassengers();
        CurrentTotalHappiness = MaxTotalHappiness;
    }

    private static int CountPassengers()
    {
        int passengerCount = 0;
        foreach (PassengerTask passenger in GameObject.FindObjectsOfType<PassengerTask>())
        {
            passengerCount++;
        }
        return passengerCount;
    }

    private void Start()
    {
        onHappinessChange += ChangeHappiness;
    }

    public static void ChangeHappiness(float change)
    {
        CurrentTotalHappiness += change;

        Happiness = CurrentTotalHappiness / MaxTotalHappiness * 100f;
        Debug.Log($"{CurrentTotalHappiness} / {MaxTotalHappiness} * 100f = {Happiness}");

        if(Happiness > 100f) { Happiness = 100f; }
        if (Happiness <= 0f)
        {
            Debug.Log("GAME OVER");
            return;
        }
        HappinessText.UpdateText(Happiness);
    }
}
