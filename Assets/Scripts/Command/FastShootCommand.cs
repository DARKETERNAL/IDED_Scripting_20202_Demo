using UnityEngine;

public class FastShootCommand : ShootCommand
{
    public FastShootCommand(Transform spawnLocation, float shootForce) : base(spawnLocation, shootForce)
    {
    }

    protected override Rigidbody GetBullet()
    {
        return FastBulletPool.Instance.GetObject().Rb;
    }
}