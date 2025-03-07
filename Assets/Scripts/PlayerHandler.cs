using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public float jumpForce = 8f; // Jump force
    public LayerMask platformLayer; // Layer mask for detecting the platform
    private Rigidbody rb;
    private Transform currentPlatform;
    private Vector3 platformLastPosition;
    private bool isGrounded;
    RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckPlatform();
    }

    void CheckPlatform()
    {
        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 rayOrigin = hit.point + Vector3.down * 0.5f; // Adjust height as needed
                float rayDistance = 10f;

                if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, platformLayer))
                {
                    if (hit.collider.CompareTag("Floating"))
                    {
                        if (currentPlatform == null)
                        {
                            currentPlatform = hit.collider.transform;
                            platformLastPosition = currentPlatform.position;
                        }
                        isGrounded = true;
                    }
                }
                else
                {
                    isGrounded = false;
                    currentPlatform = null;
                }
            }
        #endif
    }
    void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - platformLastPosition;
            transform.position += platformMovement;
            platformLastPosition = currentPlatform.position;
        }
    }
}
