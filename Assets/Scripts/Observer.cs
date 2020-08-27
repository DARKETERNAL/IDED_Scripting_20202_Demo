using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void NotifyHit();

    public abstract void Register();

    public abstract void Unregister();
}