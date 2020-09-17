using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [Serializable]
    private class EnemyDataCollection
    {
        [SerializeField]
        private List<EnemyData> enemyData;

        public List<EnemyData> EnemyData { get => enemyData; private set => enemyData = value; }

        public void Init()
        {
            if (enemyData == null)
            {
                EnemyData = new List<EnemyData>();
            }
        }

        public void AddEnemyData(EnemyData item)
        {
            EnemyData.Add(item);
        }

        public bool IsEnemyDead(string key)
        {
            bool result = false;

            foreach (EnemyData item in enemyData)
            {
                if (item.EnemyKey.Equals(key))
                {
                    result = item.IsDead;
                    break;
                }
            }

            return result;
        }
    }

    [Serializable]
    public struct EnemyData
    {
        [SerializeField]
        private string enemyKey;

        [SerializeField]
        private bool isDead;

        public EnemyData(string key, bool isDead)
        {
            enemyKey = key;
            this.isDead = isDead;
        }

        public string EnemyKey { get => enemyKey; }
        public bool IsDead { get => isDead; }
    }

    private string filePath;

    private EnemyDataCollection enemyDataCol;

    public static PersistentData Instance { get; private set; }

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "enemyData.xxx");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        LoadData();
    }

    private void LoadData()
    {
        // Loading from serialized JSON string
        //string serializedEnemyData = PlayerPrefs.GetString("EnemyData", string.Empty);
        //print(serializedEnemyData);
        //enemyDataCol = JsonUtility.FromJson<EnemyDataCollection>(serializedEnemyData);

        // Loading from serialized binary file
        LoadEnemyDataInFile();

        if (enemyDataCol == null)
        {
            enemyDataCol = new EnemyDataCollection();
        }

        enemyDataCol.Init();
    }

    // Update is called once per frame
    private void Update()
    {
        //Save data to PlayerPrefs
        if (Input.GetKeyDown(KeyCode.K))
        {
            print("Saving...");
            PlayerPrefs.SetInt("HitCounter", PlayerController.Instance.JumpCount);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            print("Deleting...");
            PlayerPrefs.DeleteAll();
        }
    }

    public int LoadHitCount()
    {
        print("Loading...");
        return PlayerPrefs.GetInt("HitCounter", 0);
    }

    public void SaveEnemyDeath(string key)
    {
        print("Saving enemy death...");
        enemyDataCol.AddEnemyData(new EnemyData(key, true));

        // Saving all enemy entries as a JSON string
        //string serializedEnemyData = JsonUtility.ToJson(enemyDataCol);
        //print(serializedEnemyData);
        //PlayerPrefs.SetString("EnemyData", serializedEnemyData);

        // Saving all enemy entries as a binary file
        SaveEnemyDataInFile();

        // Saving each enemy as a separate entry
        //PlayerPrefs.SetInt(key, 1);
    }

    public bool LoadEnemyDeath(string key)
    {
        print("Loading enemy death...");

        // Load from saved collection
        return enemyDataCol.IsEnemyDead(key);

        // Load from single enemy entry
        //return PlayerPrefs.GetInt(key, 0) == 1;
    }

    private void SaveEnemyDataInFile()
    {
        print(filePath);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
        formatter.Serialize(file, enemyDataCol);
        file.Close();
    }

    private void LoadEnemyDataInFile()
    {
        print(filePath);

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            enemyDataCol = (EnemyDataCollection)formatter.Deserialize(file);
            file.Close();
        }
    }
}