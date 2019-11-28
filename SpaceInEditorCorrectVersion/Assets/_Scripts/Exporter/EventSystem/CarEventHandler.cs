using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController carController;
    public Transform carInitPosition; 

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

    public void OnStuckButtonClicked()
    {
        SessionController.Instance.ResetTimer();
        carController.TeleportToPosition(carInitPosition);

        object newEventData = SessionController.Instance.BuildErrorEventData(ErrorType.Stuck_in_Obstacle);
        EventBroadcaster.Instance.SendEventData(newEventData);
    }

    private void OnTriggerEnter(Collider other)
    {
        object newEventData = new EventData();

        if (other.gameObject.CompareTag("Round")); 
        {
            SessionController.Instance.AddRound();
            SessionController.Instance.ResetTimer();

            newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.round_end);
            Debug.Log("Add Round"); 
        }

        if (other.gameObject.CompareTag("Fall"))
        {          
            SessionController.Instance.ResetTimer();
            carController.TeleportToPosition(carInitPosition); 

            newEventData = SessionController.Instance.BuildErrorEventData(ErrorType.Fall_off_map);
        }

        EventBroadcaster.Instance.SendEventData(newEventData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        object newEventData = new EventData();

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            newEventData = SessionController.Instance.BuildEventData(carController, CarEventType.hit, collision.gameObject);
        }

        EventBroadcaster.Instance.SendEventData(newEventData);
    }
}


