#define HAS_JUMP_IMPLEMENTED
//#define HAS_SHOOT_IMPLEMENTED

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region MovementParams

    [SerializeField]
    private float movSpeed = 10F;

    [SerializeField]
    private float rotSpeed = 2F;

    private float hVal;
    private float vVal;

    #endregion MovementParams

#if HAS_JUMP_IMPLEMENTED

    #region JumpParams

    [SerializeField]
    private float jumpForce = 1F;

    private Rigidbody myRigidbody;

    private int jumpCount;

    #endregion JumpParams

#endif

#if HAS_SHOOT_IMPLEMENTED

    #region ShootParams

    [SerializeField]
    private Rigidbody bullet;

    [SerializeField]
    private Transform spawnLocation;

    [SerializeField]
    private float shootForce;
    #endregion ShootParams

#endif

    // Start is called before the first frame update
    private void Start()
    {
#if HAS_JUMP_IMPLEMENTED
        myRigidbody = GetComponent<Rigidbody>();
        //linea de prueba
       
#endif
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

#if HAS_JUMP_IMPLEMENTED

        #region Jump

        if (Input.GetButtonUp("Jump") && myRigidbody != null && jumpCount < 2)
        {
            jumpCount += 1;
            myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        #endregion Jump

#endif

#if HAS_SHOOT_IMPLEMENTED

        #region Shoot

        if (Input.GetMouseButtonDown(0) && bullet != null && spawnLocation != null)
        {
            Rigidbody bulletClone = Instantiate<Rigidbody>(bullet, spawnLocation.position, spawnLocation.rotation);
            bulletClone.AddForce(spawnLocation.forward * shootForce, ForceMode.Impulse);
        }

        #endregion Shoot

#endif
    }

#if HAS_JUMP_IMPLEMENTED

    #region JumpReset

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    #endregion JumpReset

#endif
}
