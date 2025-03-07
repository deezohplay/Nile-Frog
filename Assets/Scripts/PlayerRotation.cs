using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 0.2f; // Sensitivity of the rotation
    private Vector2 touchStart;
    private Vector2 touchDelta;

    void Update()
    {
        if (Input.touchCount > 0) // Check for touch input
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchDelta = touch.deltaPosition;
                RotatePlayer();
            }
        }
    }

    void RotatePlayer()
    {
        float rotX = touchDelta.x * rotationSpeed;
        transform.Rotate(Vector3.up, rotX);
    }
}
