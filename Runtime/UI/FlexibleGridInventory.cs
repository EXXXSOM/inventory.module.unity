using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridInventory : FlexibleGridLayout
{
    public List<GridLayoutGroup> cellGrids = new List<GridLayoutGroup>();

    public override void CalculateGridContent(Vector2 cellSize)
    {
        for (int i = 0; i < cellGrids.Count; i++)
        {
            cellGrids[i].cellSize = cellSize;
        }
    }
}
