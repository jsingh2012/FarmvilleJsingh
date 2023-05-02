using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SavedProfile
{
    public float s_wood;
    public float s_stones;
    public float s_food;

    public List<Buildinfo> BuildinfoData = new List<Buildinfo>();
}

public class Save : MonoBehaviour
{
    public SavedProfile profile;

    private GameResources resources;
    private Buildings buildgings;
    private Build build;

  
    void Awake()
    {
        build = FindObjectOfType<Build>();
        resources = FindObjectOfType<GameResources>();
        buildgings = FindObjectOfType<Buildings>();
        
    }

    private void Start()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        if(profile == null )
        {
            profile = new SavedProfile();
        }
        profile.s_wood = resources.wood;
        profile.s_stones = resources.stone;
        profile.s_food = resources.food;

        profile.BuildinfoData.Clear();
        foreach (GameObject g in buildgings.builtObject)
        {
            Buildinfo b = g.GetComponent<Building>().info;
            profile.BuildinfoData.Add(b);
        }

        Debug.Log("profile.BuildinfoData + " + profile.BuildinfoData.Count);
        BinaryFormatter bf = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fs = File.Open(path, FileMode.OpenOrCreate);
        bf.Serialize(fs, profile);

        fs.Close();
    }

    private void LoadGame()
    {
        string path = Application.persistentDataPath + "/save.dat";
        Debug.Log("LoadGame " + path);
        // File.Delete(path);
        if (!File.Exists(path))
        {
            Debug.Log("No Saved profile found");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.Open);

        SavedProfile loadedProfile = bf.Deserialize(fs) as SavedProfile;
        fs.Close();

        resources.wood = loadedProfile.s_wood;
        resources.stone = loadedProfile.s_stones;
        resources.food = loadedProfile.s_food;

        for (int i = 0; i < loadedProfile.BuildinfoData.Count; i++)
        {
            Buildinfo buildingInfo = loadedProfile.BuildinfoData[i];
            build.RebuildBuilding(buildingInfo.id, buildingInfo.connectedGridId, (int)buildingInfo.level, buildingInfo.yRot);
            Debug.Log("Re3build " + buildingInfo.id);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
            Debug.Log("Game Saved!");
        }
    }
}
