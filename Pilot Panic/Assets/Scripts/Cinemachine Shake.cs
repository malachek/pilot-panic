using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    //private CinemachineBasicMultiChannelPerlin CMPerlin;

    public float shakeTimer;
    public float shakeTimerTotal;

    public float startingIntensity;


    public void Awake()
    {
        Debug.Log($"Awake method called in CinemachineShake");
        Instance = this;
        //cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        //Debug.Log(this.transform.parent.name + $"AHHHHHHHHHHH{CMPerlin}");
    }

    public void SetCamera(CinemachineVirtualCamera cam)
    {
        cinemachineVirtualCamera = cam;
    }

    public void ShakeCamera(float intensity, float time)
    {
        Debug.Log($"ShakeCamera called in location {this.transform.parent.name}");
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    public void Update()
    {
        return;
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            return;
        }

        //if (CMPerlin is null) return;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =  Mathf.Lerp(startingIntensity, 0f, 1- (shakeTimer/ shakeTimerTotal));
    }
}
