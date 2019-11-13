using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CSVWriteRead : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    string[] rowDataTemp = new string[3];


    // Use this for initialization
    void Start () {
        // Creating First row of titles manually..
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

        // You can add up the values in as many cells as you want.
        rowDataTemp = new string[14];
        rowDataTemp[0] = "lucasgm"; // PersonID
        rowDataTemp[1] = "0"; // SessionID
        rowDataTemp[2] = "0"; // Round   
        rowDataTemp[3] = "position"; // Event
        rowDataTemp[4] = "0.1"; // Time
        rowDataTemp[5] = "-612.6097219"; // Pos x
        rowDataTemp[6] = "-962.2563498"; // Pos y
        rowDataTemp[7] = "473.4372015"; // Pos z
        rowDataTemp[8] = "-74.54190497"; // Rot x
        rowDataTemp[9] = "-546.6404533"; // Rot y
        rowDataTemp[10] = "726.1006582"; // Rot z
        rowDataTemp[11] = "907.9773203"; // Vel x
        rowDataTemp[12] = "-795.352141"; // Vel y
        rowDataTemp[13] = "-589.1964209"; // Vel z     
        rowData.Add(rowDataTemp);
        

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
