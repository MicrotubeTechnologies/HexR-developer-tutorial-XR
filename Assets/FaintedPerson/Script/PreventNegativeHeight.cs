using UnityEngine;

public class PreventNegativeHeight : MonoBehaviour
{
    public float minHeight = 0f; // Minimum allowed height

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < minHeight)
        {
            Vector3 pos = transform.position;
            pos.y = minHeight + 0.2f;
            transform.position = pos;

            // Stop downward movement
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}
