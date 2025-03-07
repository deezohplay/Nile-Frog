using UnityEngine;

public class FrogJump : MonoBehaviour
{
    public LayerMask platformLayer; // Assign the platform layer in the inspector
    public Transform raycastOrigin; // Set this to a child empty object slightly below the player
    public float raycastDistance = 0.2f; // Adjust based on player height

    private Transform currentPlatform; // Stores the platform the player is on

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit, raycastDistance, platformLayer))
        {
            if (currentPlatform == null && hit.collider.CompareTag("Floating"))
            {
                currentPlatform = hit.collider.transform;
                transform.parent = currentPlatform; // Stick to the platform
            }
        }
        else
        {
            if (currentPlatform != null)
            {
                transform.parent = null; // Detach when no longer on the platform
                currentPlatform = null;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the Raycast in Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.down * raycastDistance);
    }
}
