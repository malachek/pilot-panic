using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float shakeTimer;
    public float shakeTimerTotal;
    public float startingIntensity;


    public void Awake()
    {
        Instance = this;
      cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    }


        public void ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
                startingIntensity = intensity;
                shakeTimerTotal = time;
                shakeTimer = time;
        }


        public void Update()
        {
            if(shakeTimer > 0)
            {
                shakeTimer -=Time.deltaTime;
                if(shakeTimer <=0f)
                {
                        //Time is over

                        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                        
                       cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =  Mathf.Lerp(startingIntensity, 0f,(1- (shakeTimer/ shakeTimerTotal)));


                }

            }

        }

}
