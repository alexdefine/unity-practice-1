using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance { get; private set; }

    public string playerName = "Guest";
    public int highScore = 0;

    // Replace with your own data shape
    [Serializable]
    public class SaveData
    {
        public int highScore;
        public string playerName;
    }

    string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    void Awake()
    {
        // enforce single instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load(); // eager-load on startup (or call later when appropriate)
    }

    public void Save()
    {
        try
        {
            var json = JsonUtility.ToJson(new SaveData { highScore = highScore, playerName = playerName }, prettyPrint: false);
            File.WriteAllText(SavePath, json);
#if UNITY_EDITOR
            Debug.Log($"Saved to {SavePath}");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e}");
        }
    }

    public void Load()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                var json = File.ReadAllText(SavePath);
                var loadedData = JsonUtility.FromJson<SaveData>(json);

                playerName = loadedData.playerName;
                highScore = loadedData.highScore;
            }
            else
            {
                playerName = "Guest";
                highScore = 0;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e}");
            playerName = "Guest";
            highScore = 0;
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) Save(); // good for mobile
    }

    void OnApplicationQuit()
    {
        Save();
    }
}
