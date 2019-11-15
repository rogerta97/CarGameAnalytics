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
       
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            newEventData.eventType = CarEventType.EVENT_HIT;
         
        }
        else if (collision.gameObject.CompareTag("Round"))
        {
            SessionController.Instance.AddRound(); 
            newEventData.eventType = CarEventType.EVENT_ROUND;  
        }
        else
            newEventData.eventType = CarEventType.EVENT_NULL;

        newEventData.personID = SessionController.Instance.GetPersonID(); 
        newEventData.sessionID = SessionController.Instance.GetSessionID(); 
        newEventData.round = SessionController.Instance.GetRounds(); 
        newEventData.time = SessionController.Instance.GetSessionTime(); 

        newEventData.posX = carController.gameObject.transform.position.x;
        newEventData.posY = carController.gameObject.transform.position.y;
        newEventData.posZ = carController.gameObject.transform.position.z;

        newEventData.rotX = carController.gameObject.transform.rotation.x;
        newEventData.rotY = carController.gameObject.transform.rotation.y;
        newEventData.rotZ = carController.gameObject.transform.rotation.z;
        newEventData.rotW = carController.gameObject.transform.rotation.w;

        newEventData.velocityX = carController.gameObject.transform.rotation.x;
        newEventData.velocityY = carController.gameObject.transform.rotation.y;
        newEventData.velocityZ = carController.gameObject.transform.rotation.z;

        EventBroadcaster.Instance.SendEventData(newEventData);
    }
}


