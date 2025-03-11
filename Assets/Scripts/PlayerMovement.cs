using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{ 
    public Rigidbody rb;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float groundCheckRadius = 0.2f;

    public LayerMask groundLayer;
    public Transform groundCheck;

    bool jump;
    bool isJumping;
    bool isGrounded;

    private RaycastHit hitLogs;
    private Vector3 floatingObjectPosition;
    private Vector3 playerPosition;
    public float duration = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        playerPosition = rb.position;

        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                //isGrounded = Physics.CheckSphere(groundCheck.position,groundCheckRadius,groundLayer);
                //rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitLogs))
                {
                    if(hitLogs.collider.CompareTag("Floating"))
                    {
                        floatingObjectPosition = hitLogs.point;
                        StartCoroutine(LerpValue(playerPosition,floatingObjectPosition));
                        transform.position = floatingObjectPosition;
                        transform.parent = hitLogs.collider.transform;
                    }
                }
            }
        #endif
    }

    void OnDrawGizmos()
    {
         // Draw ground check radius in editor for visualization
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    IEnumerator LerpValue(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;
        while(timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}

