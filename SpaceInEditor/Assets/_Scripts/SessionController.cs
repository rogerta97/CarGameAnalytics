using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    static SessionController mInstance;
    private GameObject[] objectsSubscribed;

    public string personID;
    public float sessionTime;
    public float roundTime;
    public int sessionID; 
    public int rounds;

    public bool welcomeUISeen; 

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
                GameObject go = new GameObject("Session Controller");
                mInstance = go.AddComponent<SessionController>();
                mInstance.welcomeUISeen = false; 
            }
            return mInstance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        mInstance.sessionTime += Time.deltaTime;
        mInstance.roundTime += Time.deltaTime;
    }

    public void ResetTimer()
    {
        mInstance.roundTime = 0; 
    }

    public int GetSessionID()
    {
        return mInstance.sessionID; 
    }
    public float GetSessionTime()
    {
        return mInstance.sessionTime;
    }
    public string GetPersonID()
    {
        return mInstance.personID;
    }
    public int GetRounds()
    {
        return mInstance.rounds;
    }
    public void AddRound()
    {
        mInstance.rounds++;
    }
}

