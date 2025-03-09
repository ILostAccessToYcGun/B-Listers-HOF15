using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    void Start()
    {
        
    }

    void Update() //try changing rotation instead of position and child the camera
    {
        //transform.position = Vector3.MoveTowards(transform.position, 
            //new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z), 
            //speed * Time.deltaTime);

        transform.rotation = Quaternion.identity;
    }
}
