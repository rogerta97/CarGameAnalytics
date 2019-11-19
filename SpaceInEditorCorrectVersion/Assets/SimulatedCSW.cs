using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedCSW : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReceiveEvent(EventData eventData)
    {
        Debug.Log("Event Received");
        Debug.Log(eventData.eventType); 
    }
}
