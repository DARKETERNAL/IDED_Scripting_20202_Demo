using UnityEngine;

public abstract class AbstractFactory<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static AbstractFactory<T> instance;

    [SerializeField]
    private T baseObject;

    public static AbstractFactory<T> Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    public virtual T CreateItem()
    {
        T cloneObject = Instantiate<T>(baseObject);
        return cloneObject;
    }
}