using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBulletPool : MonoBehaviour, IPool<Bullet>
{
    [SerializeField]
    private int poolSize = 5;

    private List<Bullet> instancePool = new List<Bullet>();

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
        Bullet bulletClone = RetrieveItemFromFactory();
        instancePool.Add(bulletClone);
        bulletClone.gameObject.SetActive(false);
        ResetClonePosition(bulletClone);
    }

    protected virtual Bullet RetrieveItemFromFactory()
    {
        return BulletFactory.Instance.CreateItem();
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