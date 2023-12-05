using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float airMultiplier;
    public EnemyType enemySettings;
    private GameObject target;

    [Header("Ground Check")]//wer is grond
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    private int health;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        health = enemySettings.maxHealth;
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
    }
    void FixedUpdate()
    {
        GetDestination(enemySettings.movementType);
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
    private void GetDestination(string MovementType)
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (MovementType == "spiral")
        {
            Vector3 distanceVector = gameObject.transform.position - target.transform.position;
            float distanceFromPoint = distanceVector.sqrMagnitude / 15;
            Debug.Log(distanceFromPoint);
            Vector3 destination = new Vector3(1f * (Mathf.Cos(distanceFromPoint) + distanceFromPoint * Mathf.Sin(distanceFromPoint)), 0,
                1f * (Mathf.Sin(distanceFromPoint) - distanceFromPoint * Mathf.Cos(distanceFromPoint)));

            orientation.transform.LookAt(destination + target.transform.position);
            orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        }
        else
        {
            orientation.transform.LookAt(target.transform);
            orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        }
    }
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }
}
