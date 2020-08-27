using System.Runtime.ExceptionServices;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    private const int HITS_TO_NOTIFY = 3;

    public delegate void OnWallHit();

    public static event OnWallHit onWallHit;

    [SerializeField]
    private Observer[] observers;

    private int currentHitsNotified = 0;

    private bool hasRegisteredObservers;

    private void Start()
    {
        //RegisterObservers();
        hasRegisteredObservers = true;
    }

    private void RegisterObservers()
    {
        foreach (Observer observer in observers)
        {
            observer.Register();
        }

        hasRegisteredObservers = true;
    }

    private void UnregisterObservers()
    {
        hasRegisteredObservers = false;

        foreach (Observer observer in observers)
        {
            observer.Unregister();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (hasRegisteredObservers)
            {
                currentHitsNotified += 1;
                NotifyObservers();

                if (currentHitsNotified == HITS_TO_NOTIFY)
                {
                    currentHitsNotified = 0;
                    onWallHit = null;
                    //UnregisterObservers();
                }
            }

            Destroy(collision.gameObject);
        }
    }

    private void NotifyObservers()
    {
        if (onWallHit != null)
        {
            onWallHit();
        }

        //if (observers != null)
        //{
        //    foreach (Observer observer in observers)
        //    {
        //        observer.Notify();
        //    }
        //}
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RegisterObservers();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            UnregisterObservers();
        }
    }
}