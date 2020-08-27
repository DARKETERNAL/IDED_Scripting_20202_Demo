using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void NotifyHit();

    public abstract void Register(HitCounter hitCounter);

    public abstract void Unregister(HitCounter hitCounter);
}