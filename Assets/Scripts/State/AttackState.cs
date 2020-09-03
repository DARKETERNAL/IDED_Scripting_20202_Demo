using UnityEngine;

public class AttackState : IState
{
    private EnemyController owner;

    private float shootTimeElapsed;

    public AttackState(EnemyController owner)
    {
        this.owner = owner;
    }

    public void Execute()
    {
        owner.transform.LookAt(PlayerController.Instance.transform);

        shootTimeElapsed += Time.deltaTime;

        if (shootTimeElapsed >= owner.TimeToShoot)
        {
            owner.ShootCommand.Execute();
            shootTimeElapsed = 0F;
        }
    }

    public void Init()
    {
        shootTimeElapsed = 0F;
    }
}