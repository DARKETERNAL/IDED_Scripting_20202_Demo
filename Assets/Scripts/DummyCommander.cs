using UnityEngine;

public class DummyCommander : MonoBehaviour
{
    [SerializeField]
    private float shootForce = 10F;

    private ShootDecorator shootDecorator;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            shootDecorator = gameObject.AddComponent<ShootDecorator>();
            shootDecorator.Init(shootForce);
        }
    }
}