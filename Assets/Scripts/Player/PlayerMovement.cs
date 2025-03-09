using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [Space]
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    Rigidbody2D rb;
    [SerializeField] Transform transform;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        transform = this.transform;
    }

    void Update()
    {


        float forwardVelocity = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float angularVelocity = Input.GetAxis("Horizontal") * (turnSpeed * Mathf.Deg2Rad) * Time.deltaTime;

        if (rb != null)
        {
            rb.AddForce(transform.up * forwardVelocity);
            rb.AddTorque(-angularVelocity * 100);
        }
    }
}
