using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController carController;

    private bool hasStarted = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InvokeRepeating("CheckPositionEvent", 0.1f, 0.1f);      
    }

    void CheckPositionEvent()
    {
        if (SessionController.Instance.welcomeUISeen == true)
        {
            object newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.position);
            EventBroadcaster.Instance.SendEventData(newEventData);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        object newEventData = new EventData();

        if (other.gameObject.CompareTag("Round"))
        {
            SessionController.Instance.AddRound();
            SessionController.Instance.ResetTimer();

            newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.round_end);
        }

        EventBroadcaster.Instance.SendEventData(newEventData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        object newEventData = new EventData();

        if (collision.gameObject.CompareTag("Obstacle"))
        {

            newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.hit);
        }

        EventBroadcaster.Instance.SendEventData(newEventData);
    }
}


