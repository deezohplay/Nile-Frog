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

    public GameObject logPrefab;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    public float prefSpeed = 5f;
    public float destroyXLimit = 15f;
    

    void Start()
    {
        
    }

    void Update()
    {
        
        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitLogs))
                {
                    if(hitLogs.collider)
                    {
                        Debug.Log("Successfull");
                        jump = true;
                        if(jump && !isJumping)
                        {
                            jump = false;
                            isJumping = true;
                        }
                    }
                }
            }
        #endif
    }
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundCheckRadius,groundLayer);

        if(!isGrounded && isJumping)
        {
            isJumping = false;
            return;
        }
        if(isGrounded && isJumping)
        {
            transform.position = hitLogs.point;

            //rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

            /*
            Vector3 pos = hitLogs.point;
            rb.linearVelocity = new Vector3(pos.x*moveSpeed,jumpForce,pos.z*moveSpeed);
            */
            
            /*
            if(isJumping == true)
            {
                
                Vector3 pos = hitLogs.point;
                rb.linearVelocity = new Vector3(pos.x*moveSpeed,pos.y,pos.z*moveSpeed);
                Debug.Log("New");
            }
            */
            isJumping = false;
        }
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

    /*
    void OnCollisionEnter(Collider other)
    {
        if(other.CompareTag("Log"))
        {
            moveSpeed = 0.0f;
        }
    }
    */
}

