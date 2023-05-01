using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PriceTag
{
    public float price_wood;
    public float price_stone;
    public float price_food;
}

[System.Serializable]
public class Buildinfo
{
    public int id;
    public float level = 0;
    public float yRot = 0;
    public int connectedGridId;

}

public class Building : MonoBehaviour
{
    public Buildinfo info;
    public PriceTag price;

    public string objName;
    public bool placed;
    public int baseResourceGain = 1;

    private GameResources resources;

    void Awake()
    {
        resources = FindObjectOfType<GameResources>();
        baseResourceGain = 1;
    }

    private void Update()
    {
        Debug.Log("Building " + info.id + " placed "+ placed);
        if (!placed)
        {
            return;
        }
        
        switch(info.id)
        {
            case 1:
                resources.wood += ((baseResourceGain * info.level) * Time.deltaTime);
                break;
            case 2:
                resources.stone += ((baseResourceGain * info.level) * Time.deltaTime);
                break;
            case 3:
                resources.food += ((baseResourceGain * info.level) * Time.deltaTime);
                break;
        }
    }

    public void UpgradeBuilding()
    {
        info.level += 1;
        resources.wood -= price.price_wood;
        resources.stone -= price.price_stone;
        resources.food -= price.price_food;
    }
}
