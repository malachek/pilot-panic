using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using FMODUnity;

public class PlaneHandle : InteractableBehavior
{
    [SerializeField] float MaxAllignment;
    [SerializeField] float DecayRate;

    [SerializeField] TextMeshPro AllignmentText;

    public GameObject currentCamera { get; private set; }

    float Allignment;
    // Start is called before the first frame update
    void Awake()
    {
        AllignmentText.text = Allignment.ToString("AUTOPILOT: [ON]");
        Allignment = MaxAllignment;
        Debug.Log($"HAAAAAA {CinemachineShake.Instance}");
        Debug.Log($"Awake method called from {this.transform.name}");
    }

    public void SetCamera(GameObject cam)
    {
        currentCamera = cam;
        CinemachineShake.Instance.SetCamera(cam.GetComponent<CinemachineVirtualCamera>());
        Debug.Log($"Active camera: {currentCamera}");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Allignment -= DecayRate * Time.fixedDeltaTime;

        switch (Allignment)
        {
            case > 25f:
                break;

            case <= 0.01f:
                AllignmentText.text = Allignment.ToString($"Uh oh...");
                SceneManager.LoadScene("CrashLose");
                FMODUnity.RuntimeManager.PlayOneShot("event:/GameOver");
                break;

            case <= 10f:
                AllignmentText.text = Allignment.ToString($"AUTOPILOT: [OFF] T-{Allignment:0}");
                CinemachineShake.Instance.ShakeCamera(.5f);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Alarm-Disconnect", GetComponent<Transform>().position);
                break;

            case <= 15f:
                AllignmentText.text = Allignment.ToString($"AUTOPILOT: [OFF] T-{Allignment:0}");
                CinemachineShake.Instance.ShakeCamera(.2f);
                break;

            case <= 25f:
                AllignmentText.text = Allignment.ToString("AUTOPILOT: [OFF]" /*+ " 0"*/);
                CinemachineShake.Instance.ShakeCamera(.05f);
             
                break;

            default:
                AllignmentText.text = Allignment.ToString("AUTOPILOT: [ON]" /*+ " 0"*/);
                break;

        }
    }

    public override PickupableBehavior GetInteract()
    {
        if (Allignment > 0.01f && Allignment < 98.01f)
        {
            Allignment = MaxAllignment;
            AllignmentText.text = Allignment.ToString("AUTOPILOT: [ON]" /*+ " 0"*/);
            CinemachineShake.Instance.ShakeCamera(0f);
        }
        return null;
    }
    //if (Allignment <= 0f)
    //{
    //      SceneManager.LoadScene("CrashLose");
    //    return;
    //}

    //Allignment -= DecayRate * Time.fixedDeltaTime;



    //AllignmentText.text = Allignment.ToString("AUTOPILOT ON" /*+ " 0"*/);


    //if (Allignment < 100.00f)
    //{
    //    //CinemachineShake.Instance.ShakeCamera(.0f, .1f);
    //}

    //if (Allignment < 25.00f)
    //{
    //    AllignmentText.text= Allignment.ToString("AUTOPILOT OFF" /*+ " 0"*/);
    //    CinemachineShake.Instance.ShakeCamera(.05f, .1f);
    //}

    //if (Allignment < 15.00f)
    //{
    //    AllignmentText.text= Allignment.ToString("AUTOPILOT OFF" + " 0");
    //    CinemachineShake.Instance.ShakeCamera(.2f, .1f);
    //}

    //if (Allignment < 10.00f)
    //{
    //    CinemachineShake.Instance.ShakeCamera(.5f, .1f);
    //}

    //if (Allignment <= 0f)
    //{
    //    Debug.Log("Game Over");
    //    AllignmentText.text = "Game Over";
    //}





    /*public override void AssignKey(string key)
    {
        Key = key;
    }

    public override void InRange(bool isInRange)
    {
        IsInRange = isInRange;
    }*/
}
