using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MonoBehaviour AI;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;//friction

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool ableToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]//wer is grond
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public GameSettings gameSettings;

    private void Start()
    {
        //no more spinning
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ableToJump = true;
    }

    private void Update()//runs every update
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        if (gameSettings.playerAlive) MyInput();//runs the MyInput function
        SpeedLimit();//runs the SpeedLimit function
        //friction
        if (isGrounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (orientation.transform.position.y <= -200) gameObject.GetComponent<PlayerStats>().GameOver();
        if (gameSettings.playerAlive) MovePlayer();
    }

    private void MyInput()
    {
        //get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && ableToJump && isGrounded)
        {
            ableToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //calcium movement directions
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

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

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()//reset jump
    {
        ableToJump = true;
    }
}
