using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapGenerator : MonoBehaviour
{
    public CSVRead dataContainer;
    public GameObject heatmapPlaneTemplate;

    bool heatmapGenerated = false;
    // Variables to change in order to change heatmap
    Vector2 mapSize;
    int squareSize;
    // Heatmap generation
    Vector2Int heatmapSize;
    int[,] heatmap;
    // Colors
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    // Start is called before the first frame update
    void Start()
    {
        // Hardcoded static values
        mapSize.x = 500;
        mapSize.y = 500;
        // Hardcoded dynamic value (preferably a multiple of 500)
        squareSize = 25;

        heatmapSize = new Vector2Int((int)mapSize.x / squareSize, (int)mapSize.y / squareSize);
        // Map size is 500, 500. Each square will be 25 units, therefore we want 20,20 heatmap
        heatmap = new int[heatmapSize.x, heatmapSize.y];

        // Initialize to 0
        for (int i = 0; i < heatmapSize.x; i++)
            for (int j = 0; j < heatmapSize.y; j++)
                heatmap[i, j] = 0;

        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.blue;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

    }

    // Update is called once per frame
    void Update()
    {
        if (dataContainer.isFilled && !heatmapGenerated)
        {
            heatmapGenerated = true;
            PositionEventData[] posArray = dataContainer.arrPositionEvents;

            // Fill bidimensional array with amount of position events
            foreach(PositionEventData pos in posArray)
            {
                uint index_x = (uint)((pos.posX / mapSize.x) * heatmapSize.x);
                uint index_y = (uint)((pos.posZ / mapSize.y) * heatmapSize.y);

                // Extreme cases
                if (index_x >= heatmapSize.x) index_x = (uint)heatmapSize.x - 1;
                if (index_y >= heatmapSize.y) index_y = (uint)heatmapSize.x - 1;

                heatmap[index_x, index_y]++;
            }

            // Calculate double the avarage as maximum color gradient

            int totalEvents = 0;
            int validSquares = 0;

            for (int i = 0; i < heatmapSize.x; i++)
                for (int j = 0; j < heatmapSize.y; j++)
                {
                    totalEvents += heatmap[i, j];
                    if (heatmap[i, j] != 0) validSquares++;
                }

            //  If a cell has double the avarage events, it is a very populated cell, so it is the 100% of the color gradient.
            int doubleAvarage = (totalEvents / validSquares) * 2;

            // Instanciate squares with adequate size for the square size and the right pos
            for (int i = 0; i < heatmapSize.x; i++)
                for (int j = 0; j < heatmapSize.y; j++)
                {
                    int eventsAmount = heatmap[i, j];

                    Vector3 instantiatePosition = new Vector3(squareSize / 2 + squareSize * i, 10, squareSize / 2 + squareSize * j);

                    if (eventsAmount == 0) continue;

                    GameObject plane = Instantiate(heatmapPlaneTemplate, instantiatePosition, Quaternion.identity);
                    // Set scale of squareSize: The plane is 10 unites long with a 1 scale, so in order to have 1:1 scale we need to multiply by 0.1
                    plane.transform.localScale = new Vector3(0.1f * squareSize, 1, 0.1f * squareSize); //
                    // Modify red color of plane depending on "eventsAmount"
                    plane.GetComponent<MeshRenderer>().material.color = gradient.Evaluate(eventsAmount / doubleAvarage);
                }

        }
        
    }
}
