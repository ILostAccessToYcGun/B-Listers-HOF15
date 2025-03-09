using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool isAlive;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxVelocity;
    [Space]
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    Rigidbody2D rb;
    [SerializeField] Transform transform;

    [SerializeField] bool isInDeadZone;
    [SerializeField] float deadZoneTimer;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        transform = this.transform;
    }

    void Update()
    {
        if (isAlive)
        {
            if (!isInDeadZone)
            {
                float forwardVelocity = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
                float angularVelocity = Input.GetAxis("Horizontal") * turnSpeed;

                if (rb != null)
                {
                    rb.AddForce(transform.up * forwardVelocity);
                    //rb.AddTorque(-angularVelocity * 100);
                    rb.MoveRotation(rb.rotation + -(angularVelocity) * Time.fixedDeltaTime);
                    rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
                }
            }
            else
            {
                deadZoneTimer += Time.deltaTime;
            }

            if (deadZoneTimer >= 5)
            {
                isAlive = false;
            }
        }
        else
        {
            Debug.Log("dead");
        }
        
    }


    
}
