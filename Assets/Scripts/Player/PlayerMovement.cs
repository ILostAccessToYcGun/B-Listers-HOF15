using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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

    [Space]
    [Header("Zones")]
    [SerializeField] bool isInDeadZone;
    [SerializeField] bool isInGravityZone;
    [SerializeField] float deadZoneTimer;
    [SerializeField] float boostZoneMultiplier;
    [SerializeField] bool isFinished;

    [Space]
    [Header("Camera")]
    [SerializeField] Volume postProcessing;
    [SerializeField] ColorAdjustments colourAdjustments;
    [SerializeField] private float speed;
    [SerializeField] FollowPlayer followCam;
    [SerializeField] float timet = 0.0f;
    [SerializeField] Tutorial tutorial;
    //[SerializeField] SpriteRenderer[] bodySegments;

    [Space]
    [Header("Boost")]
    [SerializeField] float boostMaxVelocity;
    [SerializeField] float boostSpeed;


    public void Boost()
    {
        //increase max velocity
        boostMaxVelocity += 2.5f;
        boostSpeed += 2.5f;
    }

    public void SetGravityZoneBoolOn()
    {
        isInGravityZone = false;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        transform = this.transform;
        postProcessing.profile.TryGet<ColorAdjustments>(out colourAdjustments);
        //bodySegments = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isAlive)
        {
            if (!tutorial.tutorialPlaying)
            {
                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, (maxVelocity + boostMaxVelocity) * boostZoneMultiplier);

                if (!isInDeadZone)
                {
                    //Acceleration, decceleration ,holding s should stop, not backwards
                    float forwardVelocity = Input.GetAxisRaw("Vertical") * (moveSpeed + boostSpeed) * Time.fixedDeltaTime * boostZoneMultiplier;
                    float angularVelocity = Input.GetAxisRaw("Horizontal") * turnSpeed / boostZoneMultiplier;

                    if (!isInGravityZone)
                        rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, Time.fixedDeltaTime / 10), Mathf.Lerp(rb.linearVelocity.y, 0, Time.fixedDeltaTime / 10f));

                    if (rb != null)
                    {
                        float dotProduct = transform.up.x * rb.linearVelocity.x + transform.up.y * rb.linearVelocity.y;
                        if (dotProduct > 0 || Input.GetAxisRaw("Vertical") > 0)
                        {
                            rb.AddForce(transform.up * forwardVelocity);
                        }
                        rb.MoveRotation(rb.rotation + -(angularVelocity) * Time.fixedDeltaTime);
                        maxVelocity = 4f;
                    }
                }
                else
                {
                    deadZoneTimer += Time.deltaTime;
                    maxVelocity = 0.8f;
                }

                if (isFinished)
                {
                    timet += speed * Time.unscaledDeltaTime;
                    followCam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(followCam.GetComponent<Camera>().orthographicSize, 1f, timet);
                }


                if (deadZoneTimer >= 5)
                {
                    isAlive = false;
                }
            }
        }
        else
        {
            Debug.Log(colourAdjustments.postExposure.value);
            colourAdjustments.postExposure.value = Mathf.Lerp(colourAdjustments.postExposure.value, 12, Time.fixedDeltaTime / 10);

            //foreach (SpriteRenderer segment in bodySegments)
            //{
            //    segment.color = Color.Lerp(segment.color, Color.black, Time.fixedDeltaTime / 10);
            //}
            //bodySegments

            if (colourAdjustments.postExposure.value >= 11.5f)
            {
                Debug.Log("dead"); //change scene to title here
                SceneManager.LoadScene(0);
            }
        }

        if (boostMaxVelocity > 0)
            boostMaxVelocity = Mathf.Clamp(boostMaxVelocity - Time.fixedDeltaTime / 7.5f, 0, 100);
        if (boostSpeed > 0)
            boostSpeed = Mathf.Clamp(boostSpeed - Time.fixedDeltaTime / 7.5f, 0, 100);


    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeadZone")
        {
            isInDeadZone = true;
        }
        if (col.gameObject.tag == "GravityZone")
        {
            isInGravityZone = true;
        }
        if (col.gameObject.tag == "BoostZone")
        {
            boostZoneMultiplier = 3f;
        }
        if (col.gameObject.tag == "Die")
        {
            isAlive = false;
        }
        if (col.gameObject.tag == "Finish")
        {
            Debug.Log("finish pls");
            isFinished = true;
            followCam.ChangeTarget(col.transform, 5);
            
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeadZone")
        {
            isInDeadZone = false;
            deadZoneTimer = 0f;
        }
        if (col.gameObject.tag == "GravityZone")
        {
            Invoke("SetGravityZoneBool", 3f);
        }
        if (col.gameObject.tag == "BoostZone")
        {
            boostZoneMultiplier = 1f;
        }
    }

}
