using UnityEngine;

public class DummyCommander : MonoBehaviour
{
    [SerializeField]
    private float shootForce = 10F;

    private ShootCommand shootCommand;

    // Start is called before the first frame update
    private void Start()
    {
        shootCommand = new ShootCommand(transform, shootForce);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            shootCommand.Execute();
        }
    }
}