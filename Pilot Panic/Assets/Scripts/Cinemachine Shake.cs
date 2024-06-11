using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}

    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private bool needsReset;


    public void Awake()
    {
        needsReset = true;
        Debug.Log($"Awake method called in CinemachineShake");
        Instance = this;
    }

    public void SetCamera(CinemachineVirtualCamera cam)
    {
        if(cinemachineVirtualCamera == cam) return;
        cinemachineVirtualCamera = cam;

        if(!needsReset) return;
        
        cinemachineVirtualCamera = cam;
        ShakeCamera(0f);
        needsReset = false;
    }

    public void ShakeCamera(float intensity)
    {
        if (intensity <= .01f) needsReset = true;
        Debug.Log($"ShakeCamera called in location {this.transform.parent.name}");
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }
}
