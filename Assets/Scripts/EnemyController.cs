using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float targetDistance = 400F;

    [SerializeField]
    private float timeToShoot = 2F;

    [SerializeField]
    private Transform spawnLocation;

    [SerializeField]
    private float shootForce = 20F;

    private float shootTimeElapsed;

    private ShootCommand shootCommand;

    // Start is called before the first frame update
    private void Start()
    {
        shootCommand = new FastShootCommand(spawnLocation, shootForce);
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
                shootCommand.Execute();
                shootTimeElapsed = 0F;
            }
        }
        else
        {
            shootTimeElapsed = 0F;
        }
    }
}