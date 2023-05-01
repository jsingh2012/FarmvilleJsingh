using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int connectedBuildingId;

    public Building connectedBuilding;
    public TMP_Text resourcesText;

    private Button btn;
    private GameResources resources;

    private void Awake()
    {
        btn = GetComponent<Button>();
        resources = FindObjectOfType<GameResources>();
   
        Buildings buildings = FindFirstObjectByType<Buildings>();
        foreach (GameObject gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if (b.info.id == connectedBuildingId)
            {
                connectedBuilding = b;
                break;
            }
        }
        resourcesText.text = "Wo: " + connectedBuilding.price.price_wood + " |  St: " + connectedBuilding.price.price_stone + " | Fd: " + connectedBuilding.price.price_wood;
        Debug.Log(" Buy Button connectedBuildingId " + connectedBuildingId + " resourcesText.text " + resourcesText.text);
    }

    private void Update()
    {
        bool result = false;
        if (resources.wood >= connectedBuilding.price.price_wood && resources.stone >= connectedBuilding.price.price_stone && resources.wood >= connectedBuilding.price.price_wood)
        {
            result = true;
        }

        btn.interactable = result;
    }

}
