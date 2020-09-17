using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public delegate void OnTargetHit();
    public delegate void OnDataLoaded();

    public event OnTargetHit onTargetHit;
    public event OnDataLoaded onDataLoaded;

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

    private ShootCommand shootCommand;

    public static PlayerController Instance { get => instance; }
    public int JumpCount { get; private set; }

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

        shootCommand = new ShootCommand(spawnLocation, shootForce);

        JumpCount = PersistentData.Instance.LoadHitCount();

        if (onDataLoaded != null)
        {
            onDataLoaded();
        }
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

        if (Input.GetButtonUp("Jump") && myRigidbody != null && JumpCount < 2)
        {
            JumpCount += 1;
            myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        #endregion Jump

        #region Shoot

        if (Input.GetButtonUp("Fire1") && spawnLocation != null)
        {
            shootCommand.Execute();
        }

        #endregion Shoot
    }

    #region JumpReset

    public void UpdateScore()
    {
        JumpCount += 1;

        if (onTargetHit != null)
        {
            onTargetHit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //JumpCount = 0;
        }
    }

    #endregion JumpReset
}