using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance; // static means that the values stored in this class member will be shared by all the instances of that class
    public Color TeamColor;

    private void Awake() // Awake method is called as soon as the object is created
    {
        // this pattern is called a singleton.You use it to ensure that only a single instance of the MainManager can ever exist, so it acts as a central point of access
        if (Instance != null) // code that only alows one instance of MainManager to exist at one time
        {
            Destroy(gameObject);
        }

        Instance = this; // you can now call MainManager.Instance from any other instance
        DontDestroyOnLoad(gameObject); // lets this script not be destroyed when a new scene is loaded

        LoadColor();
    }


    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
