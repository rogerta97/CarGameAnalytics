using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVRead : MonoBehaviour
{
    public PositionEventData[] arrPositionEvents;
    public HitEvent[] arrHitEvents;
    public SessionEventData[] arrSesionEvents;
    public RoundEndEvent[] arrRoundEndEvents;
    public ErrorEvent[] arrErrorEvents;

    public bool isFilled = false;

    //We are using a static object to control the memory we use
    static string[] lineData;

    static PositionEventData positionDataReturn;
    static HitEvent hitDataReturn;
    static SessionEventData sessionDataReturn;
    static ErrorEvent errorDataReturn;
    static RoundEndEvent roundEndDataReturn;
    

    // Start is called before the first frame update
    void Start()
    {
        ReadPositionEvent();
        ReadSessionEvent();
        ReadHitEvent();
        ReadRoundEndEvent();
        ReadErrorEvent();

        isFilled = true;   
    }
    void ReadPositionEvent()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PositionEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        arrPositionEvents = new PositionEventData[lines.Length - 2]; //Unity "ReadAllText()" gets one extra row that has nothing, for some reason.

        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            positionDataReturn.sessionID = int.Parse(lineData[0]);
            positionDataReturn.round = int.Parse(lineData[1]);
            positionDataReturn.timeStamp = float.Parse(lineData[2]);
            positionDataReturn.posX = float.Parse(lineData[3]);
            positionDataReturn.posY = float.Parse(lineData[4]);
            positionDataReturn.posZ = float.Parse(lineData[5]);
            positionDataReturn.rotX = float.Parse(lineData[6]);
            positionDataReturn.rotY = float.Parse(lineData[7]);
            positionDataReturn.rotZ = float.Parse(lineData[8]);
            positionDataReturn.rotW = float.Parse(lineData[9]);
            positionDataReturn.velocityX = float.Parse(lineData[10]);
            positionDataReturn.velocityY = float.Parse(lineData[11]);
            positionDataReturn.velocityZ = float.Parse(lineData[12]);
            arrPositionEvents[i - 1] = positionDataReturn; //It's "i-1" because arrays starts with 0.             
        }
    }
    void ReadSessionEvent()
    {
        string positionPath = Application.dataPath + "/CSV/" + "SessionEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        arrSesionEvents = new SessionEventData[lines.Length - 2]; //Unity "ReadAllText()" gets one extra row that has nothing, for some reason.
        
        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            sessionDataReturn.sessionID = int.Parse(lineData[0]);
            sessionDataReturn.personID = lineData[1];
            sessionDataReturn.timeStamp = int.Parse(lineData[2]);
            sessionDataReturn.sessionEventType = int.Parse(lineData[3]);
            arrSesionEvents[i - 1] = sessionDataReturn; //It's "i-1" because arrays starts with 0.             
        }
    }
    void ReadHitEvent()
    {
        string positionPath = Application.dataPath + "/CSV/" + "HitEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        arrHitEvents = new HitEvent[lines.Length - 2]; //Unity "ReadAllText()" gets one extra row that has nothing, for some reason.
        
        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            hitDataReturn.sessionID = int.Parse(lineData[0]);
            hitDataReturn.timeStamp = float.Parse(lineData[1]);
            hitDataReturn.ObstacleID = int.Parse(lineData[2]);
            arrHitEvents[i - 1] = hitDataReturn; //It's "i-1" because arrays starts with 0.             
        }
    }
    void ReadRoundEndEvent()
    {
        string positionPath = Application.dataPath + "/CSV/" + "RoundEndEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        arrRoundEndEvents = new RoundEndEvent[lines.Length - 2]; //Unity "ReadAllText()" gets one extra row that has nothing, for some reason.
        
        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            roundEndDataReturn.sessionID = int.Parse(lineData[0]);
            roundEndDataReturn.round = int.Parse(lineData[1]);
            roundEndDataReturn.timeStamp = float.Parse(lineData[2]);
            arrRoundEndEvents[i - 1] = roundEndDataReturn; //It's "i-1" because arrays starts with 0.             
        }
    }
    void ReadErrorEvent()
    {
        string positionPath = Application.dataPath + "/CSV/" + "ErrorEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        arrErrorEvents = new ErrorEvent[lines.Length - 2]; //Unity "ReadAllText()" gets one extra row that has nothing, for some reason.       

        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            errorDataReturn.sessionID = int.Parse(lineData[0]);
            errorDataReturn.timeStamp = float.Parse(lineData[1]);
            errorDataReturn.type = GetErrorType(lineData[2]);

            arrErrorEvents[i - 1] = errorDataReturn; //It's "i-1" because arrays starts with 0.     
        }
    }

    ErrorType GetErrorType(string str)
    {
        ErrorType ret = ErrorType.None;
        string errorType = str;
        switch (errorType)
        {
            case "Stuck_in_Obstacle":
                ret = ErrorType.Stuck_in_Obstacle;
                break;

            case "Fall_off_map":
                ret = ErrorType.Fall_off_map;
                break;
        }
        return ret;
    }
    
}
