using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 2f; // Adjust jump arc
    public float jumpSpeed = 5f; // Speed of jump
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Rigidbody rb;
    public float rotationSpeed = 10f;

    private Vector3 targetPosition;
    private bool isJumping = false;
    private bool isGrounded = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isJumping && isGrounded)
        {
            DetectAndJump();
        }
    }

    void DetectAndJump()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            StartCoroutine(JumpToPosition(hit.collider.gameObject));
        }
    }

    IEnumerator JumpToPosition(GameObject targetObject)
    {
        isJumping = true;
        Vector3 startPosition = transform.position;
        targetPosition = targetObject.transform.position; // Updates in case object is moving
        float elapsedTime = 0f;
        Vector3 midPoint = (startPosition + targetPosition) / 2 + Vector3.up * jumpHeight;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * jumpSpeed;
            Vector3 nextPosition = ParabolicLerp(startPosition, midPoint, targetPosition, elapsedTime);
            rb.MovePosition(nextPosition);
            
            // Rotate towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * rotationSpeed));
            }
            
            yield return null;
        }

        rb.MovePosition(targetPosition);
        isJumping = false;
    }

    Vector3 ParabolicLerp(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        Vector3 a = Vector3.Lerp(start, mid, t);
        Vector3 b = Vector3.Lerp(mid, end, t);
        return Vector3.Lerp(a, b, t);
    }
}

