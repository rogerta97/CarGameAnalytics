using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;

enum Table
{
    NullEvent,
    PositionEvent,
    SessionEvent,
    HitEvent,
    RoundEndEvent,
    ErrorEvent
}
public class CSVWriteRead : MonoBehaviour
{
    string[] paths = new string[5];


    // Use this for initialization
    void Start () {

        paths[(int)Table.PositionEvent - 1] = Application.dataPath + "/CSV/" + "PositionEvents.csv";
        paths[(int)Table.SessionEvent - 1] = Application.dataPath + "/CSV/" + "SessionEvents.csv";
        paths[(int)Table.HitEvent - 1] = Application.dataPath + "/CSV/" + "HitEvents.csv";
        paths[(int)Table.RoundEndEvent - 1] = Application.dataPath + "/CSV/" + "RoundEndEvents.csv";
        paths[(int)Table.ErrorEvent - 1] = Application.dataPath + "/CSV/" + "ErrorEvents.csv";


        // Clear all the files
        //foreach (string path in paths)
        //    File.Delete(path);

        // TODO(Josep) : Add header to each table
        // Position Event
        string[] RowHeadersPosition = { "SessionID", "Round", "TimeStamp", "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ", "RotW", "VelocityX", "VelocityY", "VelocityZ" };
        Save(RowHeadersPosition, Table.PositionEvent);

        // Session Event
        string[] RowHeadersSession = { "SessionID", "PersonID", "TimeStamp", "Session Event Type" };
        Save(RowHeadersSession, Table.SessionEvent);

        // Hit Event
        string[] RowHeadersHit = { "SessionID", "TimeStamp", "ObstacleID" };
        Save(RowHeadersHit, Table.HitEvent);

        // Round Event
        string[] RowHeadersRound = { "SessionID", "Round", "TimeStamp"};
        Save(RowHeadersRound, Table.RoundEndEvent);

        // Error Event
        string[] RowHeadersError = { "SessionID", "TimeStamp", "Error Type" };
        Save(RowHeadersError, Table.ErrorEvent);
    }

    void ReceiveEvent(object eventData)
    {
        // Decide to which table write

       

        // Properties serialization
        FieldInfo[] properties = eventData.GetType().GetFields();

        string[] rowDataTemp = new string[properties.Length];

        int i = 0;
        foreach (FieldInfo property in properties)
            rowDataTemp[i++] = property.GetValue(eventData).ToString().Replace(',', '.');

        Save(rowDataTemp, GetTable(eventData));
    }

    Table GetTable(object eventData)
    {
        //Table currentWriteTable = Table.NullEvent;
        if (eventData is PositionEventData)
            return Table.PositionEvent;
        else if (eventData is SessionEventData)
            return Table.SessionEvent;
        else if (eventData is HitEvent)
            return Table.HitEvent;
        else if (eventData is RoundEndEvent)
            return Table.RoundEndEvent;
        else if (eventData is ErrorEvent)
            return Table.ErrorEvent;

        return Table.NullEvent;
    }

    // PersonID
    // SessionID
    // Round
    // Event
    // Time
    // Pos(x,y,z)
    // Rot(x,y,z)
    // Vel(x,y,z)
    void Save(string[] rowData, Table table) {

        string delimiter = ",";
        string filePath = getPath(table);

        File.AppendAllText(filePath, string.Join(delimiter, rowData) + ",\n");
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(Table table)
    {
        #if UNITY_EDITOR
        return paths[(int)table - 1];
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
