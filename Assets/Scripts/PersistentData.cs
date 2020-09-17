using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [Serializable]
    public class EnemyDataCollection
    {
        public EnemyData[] enemyData;

        private List<EnemyData> enemyDataList;

        public List<EnemyData> EnemyDataList { get => enemyDataList; private set => enemyDataList = value; }

        public void Init()
        {
            EnemyDataList = enemyData.ToList();
        }

        public void AddEnemyData(EnemyData item)
        {
            EnemyDataList.Add(item);
            enemyData = EnemyDataList.ToArray();
        }

        public bool IsEnemyDead(string key)
        {
            bool result = false;

            foreach (EnemyData item in enemyData)
            {
                if (item.enemyKey.Equals(key))
                {
                    result = item.isDead;
                    break;
                }
            }

            return result;
        }
    }

    [Serializable]
    public struct EnemyData
    {
        public string enemyKey;
        public bool isDead;

        public EnemyData(string key, bool isDead)
        {
            enemyKey = key;
            this.isDead = isDead;
        }
    }

    private EnemyDataCollection enemyDataCol;

    public static PersistentData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        string serializedEnemyData = PlayerPrefs.GetString("EnemyData", string.Empty);
        print(serializedEnemyData);

        enemyDataCol = JsonUtility.FromJson<EnemyDataCollection>(serializedEnemyData);

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

        string serializedEnemyData = JsonUtility.ToJson(enemyDataCol);
        print(serializedEnemyData);

        PlayerPrefs.SetString("EnemyData", serializedEnemyData);

        //PlayerPrefs.SetInt(key, 1);
    }

    public bool LoadEnemyDeath(string key)
    {
        print("Loading enemy death...");
        return enemyDataCol.IsEnemyDead(key);

        //return PlayerPrefs.GetInt(key, 0) == 1;
    }
}