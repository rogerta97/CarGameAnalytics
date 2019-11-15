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

    public EventData BuildEventData(CarController carController)
    {

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

