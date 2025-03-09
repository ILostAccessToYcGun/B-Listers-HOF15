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

            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);

            if (!isInDeadZone)
            {
                //Acceleration, decceleration ,holding s should stop, not backwards
                float forwardVelocity = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
                float angularVelocity = Input.GetAxisRaw("Horizontal") * turnSpeed;

                if (rb != null)
                {
                    

                    float dotProduct = transform.up.x * rb.linearVelocity.x + transform.up.y * rb.linearVelocity.y;
                    Debug.Log(dotProduct);
                    if (dotProduct > 0 || Input.GetAxisRaw("Vertical") > 0)
                        rb.AddForce(transform.up * forwardVelocity);
                    rb.MoveRotation(rb.rotation + -(angularVelocity) * Time.fixedDeltaTime);
                    maxVelocity = 6f;
                }
            }
            else
            {
                deadZoneTimer += Time.deltaTime;
                maxVelocity = 0.8f;
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeadZone")
        {
            isInDeadZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeadZone")
        {
            isInDeadZone = false;
        }
    }

}
