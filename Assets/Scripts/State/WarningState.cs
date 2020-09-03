using UnityEngine;

public class WarningState : IState
{
    private GameObject owner;

    public WarningState(GameObject owner)
    {
        this.owner = owner;
    }

    public void Execute()
    {
        owner.transform.LookAt(PlayerController.Instance.transform);
    }

    public void Init()
    {
    }
}