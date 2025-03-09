using UnityEngine;

public class MenuStar : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
