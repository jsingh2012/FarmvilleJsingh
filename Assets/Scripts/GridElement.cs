using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int GridId;
    public bool isOccupied;
    public Building connectedBuilding;

    private void Awake()
    {
        Build b = FindAnyObjectByType<Build>();
        for (int i = 0; i < b.grid.Length; i++)
        {
            if (b.grid[i].transform == transform)
            {
                GridId = i;
                break;
            }
        }
    }
}
