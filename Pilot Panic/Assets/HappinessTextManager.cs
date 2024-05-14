using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HappinessTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HappinessText;
    // Start is called before the first frame update
    void Awake()
    {

        HappinessText = FindObjectOfType<TextMeshProUGUI>();
        HappinessText.text = $"Happiness: {100}";
    }

    // Update is called once per frame
    public void UpdateText(float happiness)
    {
        HappinessText.text = happiness.ToString();
    }
}
