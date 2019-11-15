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
        EventData newEventData;

        newEventData.personID = "Test";
        newEventData.sessionID = 0;
        newEventData.round = 0;
       
        newEventData.time = 0;
        newEventData.posX = 0;
        newEventData.posY = 0;
        newEventData.posZ = 0;

        newEventData.rotX = 0;
        newEventData.rotY = 0;
        newEventData.rotZ = 0;
        newEventData.rotW = 0;

        newEventData.velocityX = 0;
        newEventData.velocityY = 0;
        newEventData.velocityZ = 0;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            newEventData.eventType = CarEventType.EVENT_HIT;
            EventBroadcaster.Instance.SendEventData(newEventData);
        }
        else if (collision.gameObject.CompareTag("Round"))
        {
            newEventData.eventType = CarEventType.EVENT_ROUND;
            EventBroadcaster.Instance.SendEventData(newEventData);
        }
    }
}


