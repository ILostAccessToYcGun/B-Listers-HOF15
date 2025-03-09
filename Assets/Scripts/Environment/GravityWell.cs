using Unity.VisualScripting;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    [SerializeField] float pullForce;


    private float GetDistance(Collider2D collision)
    {
        return (this.transform.position - collision.transform.position).magnitude;
    }

    private Vector3 GetPointingTransform(Collider2D collision)
    {
        return (this.transform.position - collision.transform.position).normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //as distance approaches 0,  force increases
        collision.attachedRigidbody.AddForce(GetPointingTransform(collision) * pullForce / Mathf.Clamp(GetDistance(collision), 3f, 100f));
    }
}
