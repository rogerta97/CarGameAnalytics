using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    static SessionController mInstance;
    private GameObject[] objectsSubscribed;

    string personID;
    float sessionTime;
    int sessionID; 
    int rounds;

    public EventData BuildEventData(CarController carController, CarEventType eventType, GameObject collisionGO = null)
    {
        EventData newEventData;

        newEventData.personID = Instance.GetPersonID();
        newEventData.sessionID = Instance.GetSessionID();
        newEventData.round = Instance.GetRounds();
        newEventData.time = Instance.GetSessionTime();
        newEventData.eventType = eventType;

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

        return newEventData; 
    }

    public static SessionController Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<SessionController>();
            }
            return mInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sessionTime += Time.deltaTime;
    }

    public int GetSessionID()
    {
        return sessionID; 
    }
    public float GetSessionTime()
    {
        return sessionTime;
    }
    public string GetPersonID()
    {
        return personID;
    }
    public int GetRounds()
    {
        return rounds;
    }
    public void AddRound()
    {
        rounds++;
    }
}

