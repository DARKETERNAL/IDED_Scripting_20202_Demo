using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float autoDestroyTime = 1.5F;

    private Rigidbody rb;

    public Rigidbody Rb
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            return rb;
        }
    }

    public void OnBulletGot()
    {
        Invoke("DisableBullet", autoDestroyTime);
    }

    private void DisableBullet()
    {
        Rb.velocity = Vector3.zero;
        BulletPool.Instance.Recycle(this);
    }
}