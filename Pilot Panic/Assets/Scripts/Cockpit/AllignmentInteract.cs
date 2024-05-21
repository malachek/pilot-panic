using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaneHandle : InteractableBehavior
{
    [SerializeField] float MaxAllignment;
    [SerializeField] float DecayRate;

    [SerializeField] TextMeshPro AllignmentText;

    float Allignment;
    // Start is called before the first frame update
    void Awake()
    {
        Allignment = MaxAllignment;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Allignment <= 0f)
        {
            return;
        }

        Allignment -= DecayRate * Time.fixedDeltaTime;
        AllignmentText.text = Allignment.ToString("F0") + " % on course";
        
        if (Allignment <= 0f)
        {
            Debug.Log("Game Over");
            AllignmentText.text = "Game Over";
        }

    }

    public override void Interact()
    {
        if (Allignment > 0.01f)
        {
            Allignment = MaxAllignment;
        }
    }
}
