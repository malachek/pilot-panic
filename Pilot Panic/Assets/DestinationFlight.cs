using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DestinationFlight : MonoBehaviour
{
     static DestinationFlight _instance;

    [SerializeField] float MaxFlightLength;
    [SerializeField] float PlaneSpeed;

    [SerializeField] TextMeshPro FlightLengthText;


    float FlightLength;

    // Start is called before the first frame update
    void Awake()
    {
        FlightLength = 0f;
    }

   
    public static DestinationFlight Instance()
    {
        return _instance;
    }

 void FixedUpdate()
    {
        if (FlightLength >= 99.91f)
        {
            SceneManager.LoadScene("Win");
           // HappinessManager.OnGameWin();
            return;
        }

        FlightLength += PlaneSpeed * Time.fixedDeltaTime;
        
        FlightLengthText.text = FlightLength.ToString("FLIGHT COMPLETION" + " 0");

       


        

        
    }

    

    
}
