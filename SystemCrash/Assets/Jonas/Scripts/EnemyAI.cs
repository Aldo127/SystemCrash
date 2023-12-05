using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float airMultiplier;
    private GameObject target;

    [Header("Ground Check")]//wer is grond
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
    }
    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        orientation.transform.LookAt(target.transform);
        orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        MoveCreature();
        SpeedLimit();
    }
    private void MoveCreature()
    {
        moveDirection = orientation.forward;

        //move
        if (isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //air move
        else if (!isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedLimit()//Limits speed
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits speed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
