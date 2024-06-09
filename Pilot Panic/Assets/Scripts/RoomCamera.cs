using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{

    [SerializeField] GameObject virtualCamera;
    [SerializeField] CinemachineShake shaker;

    public PlaneHandle alligner;

    private void Awake()
    {
        Debug.Log("Awake method called in RoomCamera");
        alligner = FindObjectOfType<PlaneHandle>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            virtualCamera.SetActive(true);
            alligner.SetCamera(virtualCamera);
            //shaker.AllowShaking(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //shaker.AllowShaking(false);
            virtualCamera.SetActive(false);
        }
    }

}
