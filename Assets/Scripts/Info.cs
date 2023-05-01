using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        }
        
    }

    public void OnBtnUpgrade()
    {
        if(selectedBuilding)
        {
            selectedBuilding.UpgradeBuilding();
        }
    }
}
