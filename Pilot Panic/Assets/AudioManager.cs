using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class AudioManager : MonoBehaviour
{
    [SerializeField] EventReference FootstepEvent;
    [SerializeField] float rate;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerMovement PlayerMovement;

    // [SerializeField] EventReference MaleTaskEvent;
    // [SerializeField] PassengerBehavior DrakeBehavior;

   // private bool hasPlayedMaleSound = false;
    float time;

    public void PlayFootstep()
    {
        RuntimeManager.PlayOneShotAttached(FootstepEvent, Player);

    }

    //  public void PlayMaleTask()
    // {
    //     RuntimeManager.PlayOneShotAttached(MaleTaskEvent, Player);

    // }

    void Update()
    {
       time +=Time.deltaTime;

       if(PlayerMovement.isWalking)
       {
            if(time >=rate)
            {
                PlayFootstep();
                time = 0;

            }

       }

       if(PlayerMovement.isWalking==false)
       {
        return;

       }

        // if(DrakeBehavior.IsAssignedTask)
        // {
          
        //         Debug.Log("Assign Task is RUNNING");
        //         FMODUnity.RuntimeManager.PlayOneShot("event:/Male Task", GetComponent<Transform>().position);
       
        // }

    }
}
