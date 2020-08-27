using UnityEngine;
using UnityEngine.UI;

public class UIManager : Observer
{
    [SerializeField]
    private Text label;

    private int count;

    public override void NotifyHit()
    {
        HitCounter.onWallHit -= NotifyHit;
        count += 1;
        UpdateCount();
    }

    private void UpdateCount()
    {
        if (label != null)
        {
            label.text = count.ToString();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        HitCounter.onWallHit += NotifyHit;
        UpdateCount();
    }

    private void PrintAnything()
    {
        print("Anything");
    }

    public override void Register()
    {
        HitCounter.onWallHit += NotifyHit;
    }

    public override void Unregister()
    {
        HitCounter.onWallHit -= NotifyHit;
    }
}