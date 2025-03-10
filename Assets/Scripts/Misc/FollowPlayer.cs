using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    void Start()
    {
        
    }

    void Update()
    { 
        transform.rotation = Quaternion.identity;
    }
}
