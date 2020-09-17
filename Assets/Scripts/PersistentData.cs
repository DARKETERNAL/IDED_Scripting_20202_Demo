using UnityEngine;

public class PersistentData : MonoBehaviour
{
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
        PlayerPrefs.SetInt(key, 1);
    }

    public bool LoadEnemyDeath(string key)
    {
        print("Loading enemy death...");
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
}