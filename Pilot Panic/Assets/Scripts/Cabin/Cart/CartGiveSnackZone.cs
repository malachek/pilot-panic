using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartGiveSnackZone : MonoBehaviour
{
    [SerializeField] TextMeshPro chipsText; ////////////////////////////////////
    public List<string> Snacks { get; private set; }
    
    private int capacity;

    // Start is called before the first frame update
    void Start()
    {
        Snacks = new List<string>();
        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Passenger"))
        {
            PassengerBehavior passenger = collision.gameObject.GetComponent<PassengerBehavior>();
            //if(passenger.MyState != PassengerBehavior.PassengerState.Idle)
            if (!passenger.IsAssignedTask) { return; }

            if (passenger.IsAcceptedTask)
            {
                string passengerTask = passenger.MyTask.name;
                for (int i = 0; i < Snacks.Count; i++)
                {
                    Debug.Log(Snacks[i]);
                    if (passengerTask.Equals(Snacks[i]))
                    {
                        Snacks.RemoveAt(i);
                        passenger.CompleteTask(true);
                        UpdateText();
                        return;
                    }
                }
            }
        }
    }
    public void PickUp(string snack)
    {
        Debug.Log("Cart picked up " + snack);
        if(Snacks.Count < capacity)
        {
            Snacks.Add(snack);
            UpdateText();
        }
    }

    private void UpdateText()
    {
        chipsText.text = $"Chips: {Snacks.Count} / {capacity}";
    }

    public void SetCapacity(int _capacity)
    {
        capacity = _capacity;
    }
}
