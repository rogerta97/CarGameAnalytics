using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVRead : MonoBehaviour
{    
    ArrayList arrPositionEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
        string positionPath = Application.dataPath + "/CSV/" + "PositionEvents.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());        
        string[] lines = fileData.Split("\n"[0]);        
        string[] lineData = (lines[1].Trim()).Split(","[0]);
        PositionEventData positionEventData;
        positionEventData.sessionID = int.Parse(lineData[0]);
        positionEventData.round = int.Parse(lineData[1]);
        positionEventData.timeStamp = float.Parse(lineData[2]);
        positionEventData.posX = float.Parse(lineData[3]);
        positionEventData.posY = float.Parse(lineData[4]);
        positionEventData.posZ = float.Parse(lineData[5]);
        positionEventData.rotX = float.Parse(lineData[6]);
        positionEventData.rotY = float.Parse(lineData[7]);
        positionEventData.rotZ = float.Parse(lineData[8]);
        positionEventData.rotW = float.Parse(lineData[9]);
        positionEventData.velocityX = float.Parse(lineData[10]);
        positionEventData.velocityY = float.Parse(lineData[11]);
        positionEventData.velocityZ = float.Parse(lineData[12]);
        Debug.Log(positionEventData.velocityZ.ToString());




    }
}
