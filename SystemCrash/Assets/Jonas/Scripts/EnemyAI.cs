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
    private int age = 0;


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
        if (orientation.transform.position.y <= -200) Destroy(gameObject);
        age += 1;
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
            //Debug.Log(distanceFromPoint);
            Vector3 destination = new Vector3(1f * (Mathf.Cos(distanceFromPoint) + distanceFromPoint * Mathf.Sin(distanceFromPoint)), 0,
                1f * (Mathf.Sin(distanceFromPoint) - distanceFromPoint * Mathf.Cos(distanceFromPoint)));

            orientation.transform.LookAt(destination + target.transform.position);
            orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        }
        else if (MovementType == "flyCircle")
        {
            float a = 8;
            float b = 4;
            float c = 6;
            Vector3 destination = new Vector3(
                (a + b) * Mathf.Cos(age) - c * Mathf.Cos((a / b + age) * age), 8,
                (a + b) * Mathf.Sin(age) - c * Mathf.Sin((a / b + age) * age));
            orientation.transform.LookAt(destination + target.transform.position);
        }
        else if (MovementType == "forward")
        {
            orientation.transform.LookAt(target.transform);
            orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        }
    }
    public void Damage(int amount)
    {
        health -= amount;
        if (gameObject.GetComponent<ParticleSystem>()) gameObject.GetComponent<ParticleSystem>().Play();
        Debug.Log("Hit!");
        //gameObject.GetComponent<ParticleSystem>()?.Play();
        if (health <= 0) Destroy(gameObject);
    }
}
