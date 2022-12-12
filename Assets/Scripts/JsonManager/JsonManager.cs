using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance;
    public JsonData data;
    string path;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        path = Application.persistentDataPath + "/" + "saveData.json";
        data = new JsonData();
        Load();
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(data, true);
        var file = File.CreateText(path);

        file.Write(json);

        file.Close();
    }

    public void Load()
    {
        if (!File.Exists(path)) return;
        
        var json = File.ReadAllText(path);

        data = JsonUtility.FromJson<JsonData>(json);
    }
}
