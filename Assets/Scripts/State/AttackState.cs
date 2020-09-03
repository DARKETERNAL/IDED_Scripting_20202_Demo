using UnityEngine;

public class AttackState : IState
{
    private EnemyController owner;

    private float shootTimeElapsed;

    private ShootCommand shootCommand;

    public AttackState(EnemyController owner)
    {
        this.owner = owner;
        shootCommand = new FastShootCommand(owner.SpawnLocation, owner.ShootForce);
    }

    public void Execute()
    {
        owner.transform.LookAt(PlayerController.Instance.transform);

        shootTimeElapsed += EnemyController.SM_EXECUTE_RATE;

        if (shootTimeElapsed >= owner.TimeToShoot)
        {
            shootCommand.Execute();
            shootTimeElapsed = 0F;
        }
    }

    public void Init()
    {
        shootTimeElapsed = 0F;
    }
}