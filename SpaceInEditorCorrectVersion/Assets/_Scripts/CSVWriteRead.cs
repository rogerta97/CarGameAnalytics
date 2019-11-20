﻿using UnityEngine;
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
    Table currentWriteTable = Table.PositionEvent;
    string[] paths = new string[5];


    // Use this for initialization
    void Start () {

        paths[(int)Table.PositionEvent - 1] = Application.dataPath + "/CSV/" + "PositionEvents.csv";
        paths[(int)Table.SessionEvent - 1] = Application.dataPath + "/CSV/" + "SessionEvents.csv";
        paths[(int)Table.HitEvent - 1] = Application.dataPath + "/CSV/" + "HitEvents.csv";
        paths[(int)Table.RoundEndEvent - 1] = Application.dataPath + "/CSV/" + "RoundEndEvents.csv";
        paths[(int)Table.ErrorEvent - 1] = Application.dataPath + "/CSV/" + "ErrorEvents.csv";


        // Clear all the files
        foreach (string path in paths)
            File.Delete(path);

        // TODO(Josep) : Add header to each table
        // Position Event
        currentWriteTable = Table.PositionEvent;
        string[] RowHeadersPosition = { "SessionID", "Round", "TimeStamp", "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ", "RotW", "VelocityX", "VelocityY", "VelocityZ" };
        Save(RowHeadersPosition);

        // Session Event
        currentWriteTable = Table.SessionEvent;
        string[] RowHeadersSession = { "SessionID", "PersonID", "TimeStamp", "Session Event Type" };
        Save(RowHeadersSession);

        // Hit Event
        currentWriteTable = Table.HitEvent;
        string[] RowHeadersHit = { "SessionID", "TimeStamp", "ObstacleID" };
        Save(RowHeadersHit);

        // Round Event
        currentWriteTable = Table.RoundEndEvent;
        string[] RowHeadersRound = { "SessionID", "Round", "TimeStamp"};
        Save(RowHeadersRound);

        // Error Event
        currentWriteTable = Table.ErrorEvent;
        string[] RowHeadersError = { "SessionID", "TimeStamp", "Error Type" };
        Save(RowHeadersError);
    }

    void ReceiveEvent(object eventData)
    {
        // Decide to which table write

        if (eventData is PositionEventData)
            currentWriteTable = Table.PositionEvent;
        else if (eventData is SessionEventData)
            currentWriteTable = Table.SessionEvent;
        else if (eventData is HitEvent)
            currentWriteTable = Table.HitEvent;
        else if (eventData is RoundEndEvent)
            currentWriteTable = Table.RoundEndEvent;
        else if (eventData is ErrorEvent)
            currentWriteTable = Table.ErrorEvent;



        // Properties serialization
        FieldInfo[] properties = eventData.GetType().GetFields();

        string[] rowDataTemp = new string[properties.Length];

        int i = 0;
        foreach (FieldInfo property in properties)
            rowDataTemp[i++] = property.GetValue(eventData).ToString().Replace(',', '.');

        Save(rowDataTemp);
    }

    // PersonID
    // SessionID
    // Round
    // Event
    // Time
    // Pos(x,y,z)
    // Rot(x,y,z)
    // Vel(x,y,z)
    void Save(string[] rowData) {

        string delimiter = ",";
        string filePath = getPath();

        File.AppendAllText(filePath, string.Join(delimiter, rowData) + ",\n");
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return paths[(int)currentWriteTable - 1];
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
