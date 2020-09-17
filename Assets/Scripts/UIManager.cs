using UnityEngine;
using UnityEngine.UI;

public class UIManager : Observer
{
    [SerializeField]
    private Text label;

    public override void NotifyHit()
    {
        UpdateCount();
    }

    private void UpdateCount()
    {
        if (label != null)
        {
            label.text = PlayerController.Instance.JumpCount.ToString();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Register();
        UpdateCount();
    }

    public override void Register()
    {
        PlayerController.Instance.onTargetHit += NotifyHit;
        PlayerController.Instance.onDataLoaded += LoadData;
    }

    private void LoadData()
    {
        PlayerController.Instance.onDataLoaded -= LoadData;
        UpdateCount();
    }

    public override void Unregister()
    {
        PlayerController.Instance.onTargetHit -= NotifyHit;
    }
}