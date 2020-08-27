using UnityEngine;

public class ShootCommand : ICommand
{
    private Transform spawnLocation;
    private float shootForce;

    public ShootCommand(Transform spawnLocation, float shootForce)
    {
        this.spawnLocation = spawnLocation;
        this.shootForce = shootForce;
    }

    public void Execute()
    {
        Rigidbody bulletClone = BulletPool.Instance.GetObject().Rb;
        bulletClone.transform.position = spawnLocation.position;
        bulletClone.transform.rotation = spawnLocation.rotation;
        bulletClone.AddForce(spawnLocation.forward * shootForce, ForceMode.Impulse);
    }
}