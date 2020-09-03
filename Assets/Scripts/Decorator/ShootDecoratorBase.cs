using UnityEngine;

public abstract class ShootDecoratorBase : MonoBehaviour, IDecorator
{
    protected float shootForce = 0;

    protected ShootCommand shootCommand;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        shootCommand = new ShootCommand(transform, shootForce);
    }

    public virtual void Execute()
    {
        shootCommand.Execute();
    }
}