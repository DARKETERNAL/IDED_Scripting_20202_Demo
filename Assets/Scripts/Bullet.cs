using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float autoDestroyTime = 1.5F;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, autoDestroyTime);
    }
}