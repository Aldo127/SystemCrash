using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //mouse sensitivity
    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;
    public GameObject mainCamera;

    float xRotation;
    float yRotation;

    public GameSettings gameSettings;
    private float zRotation;

    private void Start()
    {
        //prevent the cursor from leaving the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera.transform.localPosition = new Vector3(0f, 0f, -1f * gameSettings.cameraDistanceFromPlayer);
    }
    private void Update()
    {
        
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        //y is left and right, x is up and down
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90); //prevent the player from looking too far up and looking behind them

        //apply numbers to the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, zRotation);
    }
    private void FixedUpdate()
    {
        if (gameSettings.gameIsActive &&  gameSettings.playerAlive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!gameSettings.playerAlive) zRotation = 90f;
        else zRotation = 0f;
    }
}