using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarCollisionHandler : MonoBehaviour
{
    private CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EventData newEventData = new EventData();

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.EVENT_HIT);
        }
        
        EventBroadcaster.Instance.SendEventData(newEventData);
    }
}


