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

    private AIShootDecorator shootDecorator;

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= targetDistance)
        {
            if (shootDecorator == null)
            {
                shootDecorator = spawnLocation.gameObject.AddComponent<AIShootDecorator>();
                shootDecorator.Init(targetDistance, timeToShoot, shootForce);
            }
        }
        else if (shootDecorator != null)
        {
            Destroy(shootDecorator);
            shootDecorator = null;
        }
    }
}