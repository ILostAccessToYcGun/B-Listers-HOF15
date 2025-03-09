using UnityEngine;

public class TrailPickup : MonoBehaviour
{
    [SerializeField] bool isPickedUp = false;
    [SerializeField] GameObject player;
    [SerializeField] float magnetSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private bool ConsumeCheck()
    {

        return (player.transform.position - transform.position).magnitude < 0.1f;
    }

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPickedUp = true;
        }
    }

    private void Update()
    {
        if (isPickedUp)
        {
            magnetSpeed += Time.deltaTime;
            Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime + magnetSpeed);
            Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime + magnetSpeed);

            Vector2 magnetPos = new Vector2(Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime + magnetSpeed), Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime + magnetSpeed));

            transform.position = magnetPos;

            if (ConsumeCheck())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
