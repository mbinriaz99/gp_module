using UnityEngine;

public class PigTarget : MonoBehaviour
{
    public float breakForce = 2.5f;

    void OnCollisionEnter(Collision collision)
    {
        // Agar Bird ya Cylinders zor se hit karein toh Target Destroy ho jaye
        if (collision.relativeVelocity.magnitude > breakForce)
        {
            Debug.Log("Target Defeated!");
            Destroy(gameObject, 0.1f);
        }
    }
}