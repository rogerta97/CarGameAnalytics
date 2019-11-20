using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorType
{
    None,
    Stuck_in_Obstacle,
    Fall_off_map
}
public enum CarEventType
{
    position,
    hit,
    round_end,
    event_null,
    session_start,
    event_error
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

public struct PositionEventData
{
    public int sessionID;
    public int round;
    public float timeStamp;
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
    public float velocityX, velocityY, velocityZ;
}

public struct SessionEventData
{
    public int sessionID;
    public string personID;
    public float timeStamp;
    public int sessionEventType; // 0 = sessionStart / 1 = sessionEnd. This is stupid as fuck, but i didn't decide it  
}

public struct HitEvent
{
    public int sessionID;
    public float timeStamp;
    public int ObstacleID; // From 1 to last obstacle number
}

public struct RoundEndEvent
{
    public int sessionID;
    public int round;
    public float timeStamp;
}

public struct ErrorEvent
{
    public int sessionID;
    public float timeStamp;
    public ErrorType type;
}

