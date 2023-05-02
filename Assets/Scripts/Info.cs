using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Info : MonoBehaviour
{
    public TMP_Text nameText;
    public Button btnDestroy;
    public Button btnUpgrade;

    private Build build;
    private Building selectedBuilding;
    private GameResources resources;

   
    void Awake()
    {
        build = FindFirstObjectByType<Build>();
        resources = FindObjectOfType<GameResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if(build.curSelectedGridElement != null && build.curSelectedGridElement.connectedBuilding != null)
        {
            selectedBuilding = build.curSelectedGridElement.connectedBuilding;
            nameText.text = selectedBuilding.objName + " Level " + selectedBuilding.info.level;
        } else
        {
            nameText.text = "No building Selected";
            selectedBuilding = null;
        }

        bool result = false;
        if (selectedBuilding)
        {
            if (resources.wood >= selectedBuilding.price.price_wood && resources.stone >= selectedBuilding.price.price_stone && resources.wood >= selectedBuilding.price.price_wood)
            {
                result = true;
            }
        }
        btnDestroy.interactable = result;
        btnUpgrade.interactable = result;
    }

    public void OnBtnUpgrade()
    {
        if(selectedBuilding)
        {
            selectedBuilding.UpgradeBuilding();
        }
    }

    public void OnBtnDestroy()
    {
        if (selectedBuilding)
        {
            build.curSelectedGridElement.isOccupied = false;
            build.buildings.builtObject.Remove(selectedBuilding.gameObject);
            Destroy(selectedBuilding.gameObject);
        }
    }
}
