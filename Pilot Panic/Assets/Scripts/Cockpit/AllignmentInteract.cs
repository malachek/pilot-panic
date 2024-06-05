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
            HappinessManager.OnGameOver();
            return;
        }

        Allignment -= DecayRate * Time.fixedDeltaTime;
        
         

        AllignmentText.text = Allignment.ToString("AUTOPILOT ON" /*+ " 0"*/);


         if (Allignment < 100.00f)
        {
            

            CinemachineShake.Instance.ShakeCamera(.0f, .1f);
        
        }

        if (Allignment < 25.00f)
        {
            AllignmentText.text= Allignment.ToString("AUTOPILOT OFF" /*+ " 0"*/);

            CinemachineShake.Instance.ShakeCamera(.05f, .1f);
        
        }

          if (Allignment < 15.00f)
        {
            

            CinemachineShake.Instance.ShakeCamera(.2f, .1f);
        
        }

         if (Allignment < 10.00f)
        {
            

            CinemachineShake.Instance.ShakeCamera(.5f, .1f);
        
        }

        if (Allignment <= 0f)
        {
            Debug.Log("Game Over");
            AllignmentText.text = "Game Over";
        }

    }

    public override PickupableBehavior GetInteract()
    {
        if (Allignment > 0.01f && Allignment < 98.01f)
        {
            Allignment = MaxAllignment;


        }
        return null;
    }

    /*public override void AssignKey(string key)
    {
        Key = key;
    }

    public override void InRange(bool isInRange)
    {
        IsInRange = isInRange;
    }*/
}
