using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float moveSpeed;
    public float airMultiplier;
    public EnemyType enemySettings;
    private GameObject target;

    [Header("Ground Check")]//wer is grond
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;
    public bool wasThrown = false;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    private int health;
    private float age = 0;
    private int attackCooldown;
    [SerializeField] private GameSettings gameSettings;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        health = enemySettings.maxHealth;
        moveSpeed = enemySettings.speed * gameSettings.enemySpeedMultiplier;
        attackCooldown = enemySettings.attackCooldown;
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
    }
    void FixedUpdate()
    {
        if (!gameSettings.gameIsActive) Destroy(gameObject);
        if (orientation.transform.position.y <= -200) Death();
        age += 1;
        if (age == enemySettings.lifetime) Death();
        
        if (wasThrown == true)
        {
            rb.useGravity = true;
            attackCooldown = enemySettings.attackCooldown;
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVelocity.magnitude < moveSpeed) wasThrown = false;
        }
        else
        {
            rb.useGravity = enemySettings.usesGravity;
            attackCooldown -= 1;
            GetDestination(enemySettings.movementType);
            MoveCreature();
            SpeedLimit();
            if (attackCooldown <= 0) Attack(enemySettings.attackType);
        }
        
    }
    private void MoveCreature()
    {
        moveDirection = orientation.forward;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //move
        //if (isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //air move
        //else if (!isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedLimit()//Limits speed
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        //limits speed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, limitedVelocity.y, limitedVelocity.z);
            //if (rb.useGravity == false) rb.velocity = new Vector3(limitedVelocity.x, limitedVelocity.y, limitedVelocity.z);
            //else rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    private void GetDestination(string MovementType)
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (MovementType == "spiral")
        {
            Vector3 distanceVector = gameObject.transform.position - target.transform.position;
            float distanceFromPoint = distanceVector.sqrMagnitude / 13;
            //Debug.Log(distanceFromPoint);
            Vector3 destination = new Vector3(1f * (Mathf.Cos(distanceFromPoint) + distanceFromPoint * Mathf.Sin(distanceFromPoint)), 0,
                1f * (Mathf.Sin(distanceFromPoint) - distanceFromPoint * Mathf.Cos(distanceFromPoint)));

            orientation.transform.LookAt(destination + target.transform.position);
            orientation.transform.eulerAngles = new Vector3(0, orientation.transform.eulerAngles.y);
        }
        else if (MovementType == "flyCircle")
        {
            float a = 9;
            float b = 3;
            float c = 6;
            //Vector3 destination = new Vector3(
            //    (a + b) * Mathf.Cos(age / 64) - c * Mathf.Cos((a / b + age / 64) * age / 64), 6,
            //    (a + b) * Mathf.Sin(age / 64) - c * Mathf.Sin((a / b + age / 64) * age / 64));
            Vector3 destination = new Vector3(
                (a + b) * Mathf.Cos(age / 64) - c * Mathf.Cos(((a + b) / b) * age / 64), 6,
                (a + b) * Mathf.Sin(age / 64) - c * Mathf.Sin(((a + b) / b) * age / 64));
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

        if (health <= 0) Death();
        else if (enemySettings.weak == true && wasThrown == false)
        {
            rb.AddForce(orientation.transform.forward * enemySettings.speed * -20, ForceMode.Force);
            wasThrown = true; 
        }
    }

    public void Death()
    {
        for (int i = 0; i < enemySettings.bitValue; i++)
        {
            GameObject newBit = Instantiate(gameSettings.bitPrefab, transform.position, Random.rotation);
            //newBit.transform.rotation = Random.rotation;
            newBit.SetActive(true);
            newBit.GetComponent<Rigidbody>().AddForce(newBit.transform.forward * 280f, ForceMode.Force);
        }
        Destroy(gameObject);
    }

    public void Attack(string AttackType)
    {
        Vector3 distanceVector = gameObject.transform.position - target.transform.position;
        float distanceFromPoint = distanceVector.sqrMagnitude;
        if (AttackType == "ranged")
        {
            GameObject newProjectile = Instantiate(enemySettings.projectilePrefab, transform.position, transform.rotation);
            newProjectile.GetComponent<EnemyAI>().orientation.transform.LookAt(target.transform);
            newProjectile.SetActive(true);
            attackCooldown = enemySettings.attackCooldown;
        }
        else if (AttackType == "break")
        {
            if (distanceFromPoint <= 2)
            {
                attackCooldown = enemySettings.attackCooldown;
                target.GetComponent<PlayerStats>().Damage(enemySettings.damage);
                Damage(enemySettings.damage);
            }
        }
        else
        {
            if (distanceFromPoint <= 2)
            {
                attackCooldown = enemySettings.attackCooldown;
                target.GetComponent<PlayerStats>().Damage(enemySettings.damage);
            }
        }
        
        

    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyAI>())
        {
            if (enemySettings.canThrowAllies == true && attackCooldown <= 0 && collision.gameObject.GetComponentInParent<EnemyAI>().enemySettings.throwable == true)
            {
                //Debug.Log("Throw!!!");
                collision.gameObject.GetComponentInParent<EnemyAI>().orientation.transform.LookAt(target.transform);
                collision.gameObject.GetComponentInParent<Rigidbody>().AddForce(collision.gameObject.GetComponentInParent<EnemyAI>().orientation.transform.forward * 600f, ForceMode.Force);
                collision.gameObject.GetComponentInParent<EnemyAI>().wasThrown = true;
                attackCooldown = enemySettings.attackCooldown;
            }
            else
            {
                if (collision.gameObject.GetComponentInParent<EnemyAI>().enemySettings.weak == true) collision.gameObject.GetComponentInParent<EnemyAI>().Damage(0);
                if (enemySettings.weak == true) Damage(0);
            }
            
        }
    }
}
