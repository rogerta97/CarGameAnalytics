﻿using System.Collections;
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

    public object BuildErrorEventData(ErrorType errorType)
    {
        ErrorEvent newEventData;
        newEventData.sessionID = Instance.GetSessionID();
        newEventData.timeStamp = Instance.GetSessionTime();
        newEventData.type = errorType; 
        return newEventData;
    }

    public object BuildSessionEventData(int sessionEventType)
    {
        SessionEventData newEventData;
        newEventData.sessionID = Instance.GetSessionID();
        newEventData.personID = Instance.GetPersonID();
        newEventData.sessionEventType = sessionEventType;
        newEventData.timeStamp = Instance.GetSessionTime();

        return newEventData;
    }

    public object BuildEventData(UnityStandardAssets.Vehicles.Car.CarController carController, CarEventType eventType, GameObject collisionGO = null)
    {
        /*newEventData.personID = Instance.GetPersonID();
        newEventData.sessionID = Instance.GetSessionID();
        newEventData.round = Instance.GetRounds();
        newEventData.timeStamp = Instance.GetSessionTime();
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
        newEventData.velocityZ = carController.gameObject.transform.rotation.z;*/

        if (eventType == CarEventType.position)
        {
            PositionEventData newEventData;

            newEventData.sessionID = Instance.GetSessionID();
            newEventData.round = Instance.GetRounds();
            newEventData.timeStamp = Instance.GetSessionTime();

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
        else if (eventType == CarEventType.hit)
        {
            HitEvent newEventData;

            newEventData.sessionID = Instance.GetSessionID();
            newEventData.timeStamp = Instance.GetSessionTime();
            newEventData.ObstacleID = collisionGO.GetComponent<Obstacle>().ObstacleID;

            return newEventData;
        }
        else if (eventType == CarEventType.round_end)
        {
            RoundEndEvent newEventData;
            newEventData.sessionID = Instance.GetSessionID();
            newEventData.round = Instance.GetRounds();
            newEventData.timeStamp = Instance.GetSessionTime();
            return newEventData;
        }

        return false;
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
