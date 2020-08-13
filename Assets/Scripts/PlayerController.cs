using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    [SerializeField]
    private float movSpeed = 10F;

    [SerializeField]
    private float rotSpeed = 2F;

    [SerializeField]
    private float jumpForce = 1F;

    [SerializeField]
    private Transform spawnLocation;

    [SerializeField]
    private float shootForce;

    private float hVal;
    private float vVal;

    private Rigidbody myRigidbody;

    private int jumpCount;

    public static PlayerController Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region Movement

        vVal = Input.GetAxis("Vertical"); //-1F..1F

        if (vVal != 0F)
        {
            transform.Translate(transform.forward * movSpeed * vVal * Time.deltaTime, Space.World);
        }

        hVal = Input.GetAxis("Horizontal");

        if (hVal != 0F)
        {
            transform.Rotate(Vector3.up, hVal * rotSpeed * Time.deltaTime);
        }

        #endregion Movement

        #region Jump

        if (Input.GetButtonUp("Jump") && myRigidbody != null && jumpCount < 2)
        {
            jumpCount += 1;
            myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        #endregion Jump

        #region Shoot

        if (Input.GetMouseButtonDown(0) && spawnLocation != null)
        {
            Rigidbody bulletClone = BulletPool.Instance.GetObject().Rb;
            bulletClone.transform.position = spawnLocation.position;
            bulletClone.transform.rotation = spawnLocation.rotation;
            bulletClone.AddForce(spawnLocation.forward * shootForce, ForceMode.Impulse);
        }

        #endregion Shoot
    }

    #region JumpReset

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    #endregion JumpReset
}