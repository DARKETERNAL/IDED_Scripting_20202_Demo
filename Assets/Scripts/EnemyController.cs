using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public const float SM_EXECUTE_RATE = 1F;

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

    private EnemyStateMachine stateMachine;

    public float TimeToShoot { get => timeToShoot; }
    public float WarningDistance { get => warningDistance; }
    public float AttackDistance { get => attackDistance; }
    public Transform SpawnLocation { get => spawnLocation; }
    public float ShootForce { get => shootForce; }

    private void Start()
    {
        stateMachine = new EnemyStateMachine(this);
        InvokeRepeating("ExecuteSM", 0F, SM_EXECUTE_RATE);
    }

    private void ExecuteSM()
    {
        stateMachine.Execute(Vector3.Distance(transform.position, PlayerController.Instance.transform.position));
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    stateMachine.Execute(Vector3.Distance(transform.position, PlayerController.Instance.transform.position));
    //}
}