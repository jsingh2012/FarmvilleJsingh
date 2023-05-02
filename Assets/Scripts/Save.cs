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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
            Debug.Log("Game Saved!");
        }
    }
}
