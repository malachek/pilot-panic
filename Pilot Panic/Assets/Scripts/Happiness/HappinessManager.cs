using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HappinessManager : MonoBehaviour
{
    static HappinessTextManager HappinessText;
    [SerializeField] float m_MaxHappiness;
    public static float Happiness { get; private set; }

    private static float CurrentTotalHappiness;
    private static float MaxTotalHappiness;

    public delegate void OnHappinessChange(float change);
    public static OnHappinessChange onHappinessChange;

    public delegate void GameOverDelegate();
    public static GameOverDelegate OnGameOver;

    public delegate void GameWinDelegate();
    public static GameWinDelegate OnGameWin;

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


    public static HappinessManager Instance()
    {
        return _instance;
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
        foreach (PassengerBehavior passenger in GameObject.FindObjectsOfType<PassengerBehavior>())
        {
            passengerCount++;
        }
        return passengerCount;
    }

    private void Start()
    {
        onHappinessChange += ChangeHappiness;
        OnGameOver += GameOver;
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
            GameOver();
            return;
        }
        HappinessText.UpdateText(Happiness);
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("SadLose");
        _instance.Awake();
    }
    
    public static void GameWin()
    {
        SceneManager.LoadScene("Win");
        _instance.Awake();
    }

}
