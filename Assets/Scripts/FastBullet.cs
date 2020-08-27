using UnityEngine;

public class FastBullet : Bullet
{
    protected override void DisableBullet()
    {
        Rb.velocity = Vector3.zero;
        FastBulletPool.Instance.Recycle(this);
    }
}