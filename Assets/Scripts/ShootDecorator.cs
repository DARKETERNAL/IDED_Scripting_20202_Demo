using UnityEngine;

public class ShootDecorator : MonoBehaviour, IDecorator
{
    private float shootForce = 0;

    private int ammo = 20;

    private ShootCommand shootCommand;

    public void Init(float shootForce)
    {
        this.shootForce = shootForce;
    }

    // Start is called before the first frame update
    private void Start()
    {
        shootCommand = new ShootCommand(transform, shootForce);
    }

    public void Execute()
    {
        shootCommand.Execute();

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