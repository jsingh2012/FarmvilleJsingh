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

    public Buildings buildings;

    private RaycastHit mouseHit;

    private Color colOnNormal;

    private bool buildInprogress = false;
    private GameObject currentCreatedBuildable;


    void Awake()
    {
        buildings = FindObjectOfType<Buildings>();
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
        MoveBuilding();
        PlaceBuilding();
    }

    public void OnBtnCreateBuilding(int id)
    {
        if (buildInprogress)
        {
            return;
        }

        GameObject g = null;
        foreach(GameObject gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if(b.info.id == id)
            {
                g = b.gameObject;
                break;
            }
        }

        currentCreatedBuildable = Instantiate(g);
        currentCreatedBuildable.transform.rotation = Quaternion.Euler(0, transform.rotation.y - 225, 0);
        buildInprogress = true;
    }

    public void MoveBuilding()
    {
        if(!currentCreatedBuildable)
        {
            return;
        }

        currentCreatedBuildable.gameObject.layer = 2;

        if(curHoveredGridElement)
        {
            currentCreatedBuildable.transform.position = curHoveredGridElement.transform.position;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(currentCreatedBuildable);
            currentCreatedBuildable = null;
            buildInprogress = false;
        }
    }

    public void PlaceBuilding()
    {
        if(! currentCreatedBuildable || curHoveredGridElement.isOccupied)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            buildings.builtObject.Add(currentCreatedBuildable);
            curHoveredGridElement.isOccupied = true;
            Building b = currentCreatedBuildable.GetComponent<Building>();
            curHoveredGridElement.connectedBuilding = b;
            b.placed = true;
            b.info.connectedGridId = curHoveredGridElement.GridId;
            b.info.yRot = b.transform.localEulerAngles.y;

            currentCreatedBuildable = null;
            buildInprogress = false;
        }

    }
}
