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

    private void Start()
    {
        //prevent the cursor from leaving the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera.transform.localPosition = new Vector3(0f, 0f, -1f * gameSettings.cameraDistanceFromPlayer);
    }
    private void Update()
    {
        if (gameSettings.playerAlive) mainCamera.transform.eulerAngles = new Vector3(0f, 0f, 70f) ;
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        //y is left and right, x is up and down
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90); //prevent the player from looking too far up and looking behind them

        //apply numbers to the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}