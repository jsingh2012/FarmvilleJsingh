using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GridElement curSelectedGridElement;
    public GridElement curHoveredGridElement;

    public GridElement[] grid;

    [Header("Color")]
    public Color colOnHover = Color.white;
    public Color colOnOccupied = Color.red;

    private RaycastHit mouseHit;

    private Color colOnNormal;


    void Awake()
    {
        colOnNormal = grid[0].GetComponentInChildren<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("ray ");
        if (Physics.Raycast(ray, out mouseHit))
        {
            Debug.Log("mouseHit ");
            GridElement g = mouseHit.transform.GetComponent<GridElement>();
            if(!g)
            {
                if(curHoveredGridElement)
                {
                    curHoveredGridElement.GetComponent<MeshRenderer>().material.color = colOnNormal;
                    return;
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                curSelectedGridElement = g;

            }

            if (g != curHoveredGridElement)
            {
                if(!g.isOccupied)
                {
                    mouseHit.transform.GetComponent<MeshRenderer>().material.color = colOnHover;
                } else
                {
                    mouseHit.transform.GetComponent<MeshRenderer>().material.color = colOnOccupied;
                }
            }

            if(curHoveredGridElement && curHoveredGridElement != g)
            {
                curHoveredGridElement.GetComponent<MeshRenderer>().material.color = colOnNormal;
            }

            curHoveredGridElement = g;
         }
    }
}
