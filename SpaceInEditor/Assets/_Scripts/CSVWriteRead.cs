﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;

public class CSVWriteRead : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();


    // Use this for initialization
    void Start () {
        // Creating First row of titles manually..
        Save();
    }

    void ReceiveEvent(EventData eventData)
    {
        // Aqui ya haceis lo que tengais que hacer con eventData

        Debug.Log(eventData.eventType);
        Debug.Log(eventData.round);
     

        //FieldInfo[] properties = eventData.GetType().GetFields();

        //string[] rowDataTemp = new string[properties.Length];

        //int i = 0;
        //foreach(FieldInfo property in properties)
        //    rowDataTemp[i++] = property.GetValue(eventData).ToString();

        //rowData.Add(rowDataTemp);

        Save();
    }

    // PersonID
    // SessionID
    // Round
    // Event
    // Time
    // Pos(x,y,z)
    // Rot(x,y,z)
    // Vel(x,y,z)
    void Save() {

        string[][] output = new string[rowData.Count][];

        for(int i = 0; i < output.Length; i++){
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();
        
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));
        
        
        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Saved_data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
