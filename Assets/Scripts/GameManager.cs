using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.ComponentModel;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] GameObject upgradesPanel;

    public bool isGameActive { get; private set; }
    // public SaveDataClass playerUpgrades_Permanent = new SaveDataClass();


    // Game stat/score variables part.
    [DefaultValue(0)]
    public int enemyKilled 
    {
        get{ return _enemyKilled;}
        set{ Interlocked.Increment(ref _enemyKilled);}
    }
    private int _enemyKilled;

    [DefaultValue(0)]
    public int score
    {
        get{return _score;}
        set{Interlocked.Add(ref _score, value);}
    }
    private int _score;
   
    public void ResetEnemyKilled()
    {
        _enemyKilled = 0;
    }
    public void ResetScore()
    {
        _score = 0;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Set_IsGameActive(bool value)
    {
        isGameActive = value;
    }
/* 
    [System.Serializable]
    public class SaveDataClass
    {
        public int speed;
        public int health;
        public int attackRate;
        public int availablePoints;
    }

    public void SaveData()
    {
        SaveDataClass data = new SaveDataClass();

        data = playerUpgrades_Permanent;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);
            playerUpgrades_Permanent = data;
        }
    }

    public SaveDataClass GetPermenant_Upgrades()
    {
        LoadData();
        return playerUpgrades_Permanent;
    } */

    // Updates player stats (speed, health, attack rate) when player created. So this function will be called at 
    // the Start() of PlayerController script.
   /*  public void UpdatePlayerStats()
    {
        LoadData();
    } */


}
