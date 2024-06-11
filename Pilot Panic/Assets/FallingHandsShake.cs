using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHandsShake : MonoBehaviour
{
    [SerializeField] GameObject hands;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 100) < 5)
        {
            hands.transform.position = Vector3.Lerp(hands.transform.position, new Vector3(hands.transform.position.x + Random.Range(-3, 3) * 4, hands.transform.position.y - 1f, hands.transform.position.z), Random.Range(.5f, 1.5f));
        }
    }
}
