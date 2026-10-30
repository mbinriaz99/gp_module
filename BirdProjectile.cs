using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdProjectile : MonoBehaviour
{
    public float launchForce = 50f;
    public float resetDelay = 5f; // 5 seconds baad wapas aane ke liye

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 originalPos; // Bird ki original position save karne ke liye
    private Rigidbody rb;
    private bool isLaunched = false;
    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        mainCamera = Camera.main;

        // Game start hote hi Bird ki original position save kar rahe hain
        originalPos = transform.position;
    }

    void Update()
    {
        // 1. Mouse Button Dabaya
        if (Input.GetMouseButtonDown(0) && !isLaunched)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    startPos = Input.mousePosition;
                }
            }
        }

        // 2. Mouse Button Chhora
        if (Input.GetMouseButtonUp(0) && isDragging && !isLaunched)
        {
            isDragging = false;
            endPos = Input.mousePosition;

            Vector3 dragDirection = startPos - endPos;
            Vector3 launchDirection = new Vector3(dragDirection.x, 0, dragDirection.y).normalized;

            rb.isKinematic = false;
            rb.AddForce(launchDirection * launchForce * dragDirection.magnitude * 0.05f, ForceMode.Impulse);

            isLaunched = true;

            // 5 seconds baad ResetBird method ko call karein
            Invoke(nameof(ResetBird), resetDelay);
        }
    }

    // Bird ko original position par wapas laane ka method
    void ResetBird()
    {
        rb.isKinematic = true; // Physics pause karein
        rb.linearVelocity = Vector3.zero; // Movement rokein
        rb.angularVelocity = Vector3.zero; // Rotation rokein

        transform.position = originalPos; // Slingshot par wapas bhejein
        isLaunched = false; // Phir se launch karne ke liye ready

        Debug.Log("Bird reset ho gayi!");
    }
}