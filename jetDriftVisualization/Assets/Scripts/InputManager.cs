using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool debugInputs;

    public GameObject player;
    public PlayerCamera playerCamera;

    //public string axisLeftRight = "LeftRight";
    //public string axisForwardBack = "ForwardBack";
    //public string axisUpDown = "UpDown";

    public bool invertCamera;
    [Range(0.1f, 10f)]
    public float camHorizontalSensitivity = 1f;
    [Range(0.1f, 10f)]
    public float camVerticalSensitivity = 1f;
    public float scrollSpeed = 4f;
    public string camLeftRight = "Mouse X";
    public string camUpDown = "Mouse Y";
    public string mouseScroll = "Mouse ScrollWheel";

    //public string interact1 = "Interact1";
    //public string activate1 = "Activate1";

    public string pause = "Cancel";

    public bool interact1Held = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
    }

    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        MoveCamera();
        Pause();
    }

    //private void Moveplayer()
    //{
    //    Vector3 movementVector = Vector3.zero;
    //    movementVector.x = Input.GetAxis(axisLeftRight);
    //    movementVector.y = Input.GetAxis(axisUpDown);
    //    movementVector.z = Input.GetAxis(axisForwardBack);

    //    playerMovement.MovePlayer(movementVector);

    //    if (debugInputs)
    //    {
    //        Debug.Log(movementVector);
    //    }
    //}

    private void MoveCamera()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis(camLeftRight) * camHorizontalSensitivity;
        movementVector.y = Input.GetAxis(camUpDown) * camVerticalSensitivity * (invertCamera ? 1f : -1f);
        movementVector.z = -Input.GetAxis(mouseScroll) * scrollSpeed;

        //TODO: Pass vertical movement to camera, and horizontal movement to player
        //playerMovement.rotatePlayer(movementVector.x);
        playerCamera.MoveCamera(movementVector);

        if (debugInputs)
        {
            Debug.Log(movementVector);
        }
    }

    //private void Interact()
    //{
    //    float interactValue = Input.GetAxis(interact1);

    //    if (interact1Held == false && interactValue > 0.1f)
    //    {
    //        interact1Held = true;
    //        //TODO: Pass interact to player
    //        playerInteraction.InteractWithTarget();

    //        if (debugInputs)
    //        {
    //            Debug.Log("Pressed interact button!");
    //        }
    //    }

    //    if (interactValue < 0.1f)
    //    {
    //        interact1Held = false;
    //    }
    //}

    //private void Activate()
    //{
    //    float activateValue = Input.GetAxis(activate1);
    //    if (activateValue > 0.1)
    //    {
    //        playerInteraction.ActivateTarget();
    //    }
    //}

    private void Pause()
    {
        //float pauseValue = Input.GetAxis(pause);
        if(Input.GetButtonDown(pause))
        {
            SwitchMouseLock();
        }
    }

    private void SwitchMouseLock()
    {
        if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;

        Cursor.visible = !Cursor.visible;
    }
}