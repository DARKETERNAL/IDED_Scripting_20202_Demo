using UnityEngine;

public class ShootDecorator : ShootDecoratorBase
{
    private int ammo = 20;

    public void Init(float shootForce)
    {
        this.shootForce = shootForce;
    }

    public override void Execute()
    {
        base.Execute();

        ammo--;

        if (ammo == 0)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            Execute();
        }
    }
}