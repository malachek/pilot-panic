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

    float time;

    public void PlayFootstep()
    {
        RuntimeManager.PlayOneShotAttached(FootstepEvent, Player);

    }


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
    }
}
