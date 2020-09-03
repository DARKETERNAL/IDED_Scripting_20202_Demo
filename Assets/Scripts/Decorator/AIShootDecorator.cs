using UnityEngine;

public class AIShootDecorator : ShootDecoratorBase
{
    private float targetDistance;
    private float timeToShoot;

    private float shootTimeElapsed;

    public void Init(float targetDistance, float timeToShoot, float shootForce)
    {
        this.targetDistance = targetDistance;
        this.timeToShoot = timeToShoot;
        this.shootForce = shootForce;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        shootCommand = new FastShootCommand(transform, shootForce);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= targetDistance)
        {
            transform.LookAt(PlayerController.Instance.transform);

            shootTimeElapsed += Time.deltaTime;

            if (shootTimeElapsed >= timeToShoot)
            {
                Execute();
                shootTimeElapsed = 0F;
            }
        }
        else
        {
            shootTimeElapsed = 0F;
        }
    }
}