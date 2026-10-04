using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float turnSpeed = 120f;
    [SerializeField] float jumpForce = 6f;
    Rigidbody rb;
    bool isGrounded, jumpQueued;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            jumpQueued = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // A/D turn the player
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, h * turnSpeed * Time.fixedDeltaTime, 0));

        // W/S move in the direction the player faces
        Vector3 move = transform.forward * v * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (jumpQueued)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpQueued = false;
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (c.GetContact(0).normal.y > 0.5f)
            isGrounded = true;
    }

    void OnCollisionExit(Collision c) => isGrounded = false;
}