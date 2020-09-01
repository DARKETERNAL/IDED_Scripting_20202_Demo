using UnityEngine;

public class ShootCommand : ICommand
{
    protected Transform spawnLocation;
    protected float shootForce;

    public ShootCommand(Transform spawnLocation, float shootForce)
    {
        this.spawnLocation = spawnLocation;
        this.shootForce = shootForce;
    }

    protected virtual Rigidbody GetBullet()
    {
        return BulletPool.Instance.GetObject().Rb;
    }

    public void Execute()
    {
        Rigidbody bulletClone = GetBullet();
        bulletClone.transform.position = spawnLocation.position;
        bulletClone.transform.rotation = spawnLocation.rotation;
        bulletClone.AddForce(spawnLocation.forward * shootForce, ForceMode.Impulse);
    }
}