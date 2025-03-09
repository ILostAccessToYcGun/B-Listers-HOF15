using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] public float turnSpeed;

    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
