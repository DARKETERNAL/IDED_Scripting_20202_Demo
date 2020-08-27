using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour, IPool<Bullet>
{
    private static BulletPool instance;

    [SerializeField]
    private Bullet bulletBaseObject;

    [SerializeField]
    private int poolSize = 5;

    private List<Bullet> instancePool = new List<Bullet>();

    public static BulletPool Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        Fill();
    }

    public void Fill()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateItem();
        }
    }

    private void CreateItem()
    {
        Bullet bulletClone = BulletFactory.Instance.CreateItem();
        instancePool.Add(bulletClone);
        bulletClone.gameObject.SetActive(false);
        ResetClonePosition(bulletClone);
    }

    private void ResetClonePosition(Bullet clone)
    {
        clone.transform.parent = transform;
        clone.gameObject.transform.position = transform.position;
        clone.gameObject.transform.rotation = transform.rotation;
    }

    public Bullet GetObject()
    {
        if (instancePool.Count == 0)
        {
            CreateItem();
        }

        Bullet availableInstance = instancePool[0];
        availableInstance.transform.parent = null;
        availableInstance.gameObject.SetActive(true);
        availableInstance.OnBulletGot();
        instancePool.Remove(availableInstance);

        return availableInstance;
    }

    public void Recycle(Bullet poolObject)
    {
        instancePool.Add(poolObject);
        ResetClonePosition(poolObject);
        poolObject.gameObject.SetActive(false);
    }
}