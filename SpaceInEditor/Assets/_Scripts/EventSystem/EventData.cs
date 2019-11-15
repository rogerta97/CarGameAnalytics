using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CarEventType
{
    EVENT_POSITION,
    EVENT_HIT,
    EVENT_ROUND,
    EVENT_NULL
}

public struct EventData
{
    public string personID;
    public int sessionID;
    public int round;
    public CarEventType eventType;
    public float time;

    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
    public float velocityX, velocityY, velocityZ;
}

