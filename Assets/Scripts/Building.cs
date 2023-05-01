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
    public int baseResourceGain;

    private GameResources resources;

    void Awake()
    {
        resources = FindObjectOfType<GameResources>();
    }

    private void Update()
    {
        if(!placed)
        {
            return;
        }

        switch(info.id)
        {
            case 1:
                resources.wood += (baseResourceGain * info.level) * Time.deltaTime;
                break;
            case 2:
                resources.stone += (baseResourceGain * info.level) * Time.deltaTime;
                break;
            case 3:
                resources.food += (baseResourceGain * info.level) * Time.deltaTime;
                break;
        }
    }
}
