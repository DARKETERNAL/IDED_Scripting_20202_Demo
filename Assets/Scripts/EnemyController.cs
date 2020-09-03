using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("State distances")]
    [SerializeField]
    private float warningDistance = 50F;

    [SerializeField]
    private float attackDistance = 25F;

    [Header("Shoot")]
    [SerializeField]
    private float timeToShoot = 2F;

    [SerializeField]
    private Transform spawnLocation;

    [SerializeField]
    private float shootForce = 20F;

    private ShootCommand shootCommand;

    private IState idleState;
    private IState warningState;
    private IState attackState;

    private IState currentState;
    private float distanceToPlayer;

    public ShootCommand ShootCommand { get => shootCommand; }
    public float TimeToShoot { get => timeToShoot; private set => timeToShoot = value; }

    private void Start()
    {
        shootCommand = new FastShootCommand(spawnLocation, shootForce);

        idleState = new IdleState();
        warningState = new WarningState(gameObject);
        attackState = new AttackState(this);

        currentState = idleState;
    }

    private void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.Init();
    }

    // Update is called once per frame
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            if (currentState != attackState)
            {
                print("Changed to attack");
                ChangeState(attackState);
            }
        }
        else if (distanceToPlayer <= warningDistance)
        {
            if (currentState != warningState)
            {
                print("Changed to warning");
                ChangeState(warningState);
            }
        }
        else if (currentState != idleState)
        {
            print("Changed to idle");
            ChangeState(idleState);
        }

        currentState.Execute();
    }
}