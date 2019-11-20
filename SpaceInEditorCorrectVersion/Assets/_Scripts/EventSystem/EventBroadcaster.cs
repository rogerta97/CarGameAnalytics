using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroadcaster : MonoBehaviour
{
    static EventBroadcaster mInstance;
    private GameObject[] objectsSubscribed; 

    public static EventBroadcaster Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject("Event Broadcaster");
                mInstance = go.AddComponent<EventBroadcaster>();
            }
            return mInstance;
        }
    }

    private void Awake()
    {
        objectsSubscribed = GameObject.FindGameObjectsWithTag("Player");
    }

    public void SendEventData(object eventData)
    {
         foreach(GameObject currentObject in objectsSubscribed)
         { 
            currentObject.SendMessage("ReceiveEvent", eventData); 
         }
    }
}

