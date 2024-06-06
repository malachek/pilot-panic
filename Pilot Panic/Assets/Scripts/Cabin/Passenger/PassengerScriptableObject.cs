using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassengerScriptableObject", menuName = "ScriptableObjects/Passenger")]
public class PassengerScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite[] sprites;
    public Sprite[] Sprites { get => sprites; private set => sprites = value; }

    [SerializeField]
    [Range(.5f, 2f)]
    float happinessGainMultiplier;
    public float HappinessGainMultiplier { get => happinessGainMultiplier; private set => happinessGainMultiplier = value; }

    [SerializeField]
    [Range(.5f, 2f)]
    float happinessLossMultiplier;
    public float HappinessLossMultiplier { get => happinessLossMultiplier; private set => happinessLossMultiplier = value;}

    [SerializeField]
    float patienceAtZero;
    public float PatienceAtZero { get => patienceAtZero; private set => patienceAtZero = value;}

    [SerializeField]
    float[] moodMilestones;
    public float[] MoodMileStones { get => moodMilestones; private set => moodMilestones = value;}
}