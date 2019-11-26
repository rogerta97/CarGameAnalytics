using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapGenerator : MonoBehaviour
{
    public CSVRead dataContainer;
    bool heatmapGenerated = false;

    Vector2 mapSize;
    int squareSize;

    Vector2Int heatmapSize;
    int[,] heatmap;

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
                int index_x = (int)((pos.posX / mapSize.x) * heatmapSize.x);
                int index_y = (int)((pos.posZ / mapSize.y) * heatmapSize.y);

                // Extreme cases
                if (index_x >= heatmapSize.x) index_x = heatmapSize.x - 1;
                if (index_y >= heatmapSize.y) index_y = heatmapSize.x - 1;

                heatmap[index_x, index_y]++;
            }

            // Instanciate squares with adequate size for the square size and the right pos
            Debug.Log(heatmap);

        }
        
    }
}
