using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public CSVRead dataContainer;

    public GameObject arrowPrefab;
    public GameObject pathContainer; 

    public Color fastColor;
    public Color slowColor;

    [HideInInspector] public int roundShowing;
    [HideInInspector] public int maxRounds;

    private bool pathGenerated;
    private List<GameObject> arrowsInMap; 

    // Start is called before the first frame update
    void Start()
    {
        pathGenerated = false;
        arrowsInMap = new List<GameObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (dataContainer.isFilled && !pathGenerated)
        {
            pathGenerated = true;
            GeneratePath(); 
        }
    }

    public void SetPathVisible(bool setVisible)
    {
        pathContainer.SetActive(setVisible); 
    }

    public void CleanArrowsInMap()
    {
        foreach (GameObject currentArrow in arrowsInMap)
        {
            Object.Destroy(currentArrow); 
        }

        arrowsInMap.Clear(); 
    }

    public void GeneratePath()
    {
        if (arrowsInMap.Count > 0)      
            CleanArrowsInMap();
        
        PositionEventData[] posArray = dataContainer.arrPositionEvents;
        GameObject prevArrow = null;

        foreach (PositionEventData pos in posArray)
        {
            if (roundShowing != pos.round)
                continue; 

            int posArrayLenght = posArray.Length - 1;
            maxRounds = posArray[posArrayLenght - 1].round;

            // Get instance arrow
            GameObject currentArrow = GameObject.Instantiate(arrowPrefab);
            currentArrow.transform.parent = pathContainer.transform;
            arrowsInMap.Add(currentArrow); 

            // Set position and rotation
            currentArrow.transform.position = new Vector3(pos.posX, pos.posY, pos.posZ);
            currentArrow.transform.rotation = new Quaternion(pos.rotX, pos.rotY, pos.rotZ, pos.rotW);

            // Set arrow color 
            Color arrowColor = GetColorFromArrowsDistance(prevArrow, currentArrow);
            currentArrow.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = arrowColor;

            prevArrow = currentArrow;
        }
    }

    Color GetColorFromArrowsDistance(GameObject currentArrow, GameObject prevArrow)
    {
        //Hardcoded max distance value 
        float maxDistance = 1.5f;

        // Sanity Check 
        if (currentArrow == null || prevArrow == null)
            return new Color(); 

        Vector3 arrowsDistanceVector = currentArrow.transform.position - prevArrow.transform.position;
        float distanceValue = arrowsDistanceVector.magnitude;

        float distancePerc = distanceValue / maxDistance;

        Color slowColorAmount = slowColor * (1 - distancePerc); 
        Color fastColorAmount = fastColor * (distancePerc);

        Color retColor = new Color();
        retColor = slowColorAmount + fastColorAmount; 
        
        return retColor;
    }
}
