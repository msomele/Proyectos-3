using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : MonoBehaviour

{
//-----------------------STATS-----------------------------------//

    public float attackDamage = 5;
    public float hp = 100; 
    

//-----------------------ASSIGNABLES-----------------------------//
    [Header("Assignables")]
    [Tooltip("Camera assigned 2 the player")]
    public GameObject myCamera;
    [Tooltip("Players rigidbody")]
    public Rigidbody rb;

//-----------------------MOVEMENT-------------------------------//
    [Header("Movement")]
    [Tooltip("Speed assigned 2 the player")]
    public float speed;
    [SerializeField][Tooltip("Input vector2")]
    private float movingThreshold = 0.1f;
    public Vector2 addVel;
    float hori, verti;
    //-----------------------LOOKING-------------------------------//
    [Header("Looking")]
    [SerializeField]
    [Tooltip("Looking rotation euler verctor")]
    private Vector3 lookingInput;
    private float sensitivity = 120f;
    private Vector3 pointToLook;
    private float lookingThreshold = 0.1f;
    private Quaternion rot;
    private Quaternion current;
    //--------------------JUMPING----------------------------------//
    [Header("Jumping")]
    public float jumpForce;
    public float ownGravity = 10;
    public bool isGrounded;
    public bool jumpInput = false;
    public bool wantJumpinNormal = false;
    [SerializeField] private GameObject currentFloor;
    RaycastHit hit;
    //-------------------INPUTS-----------------------------------//
    [Header("Inputs")]
    public InputBarbarian controls;
    public bool gamepadSelected;
    public bool keyboardMouseSelected = true;
    //------------------------CAMERA------------------------------//
    [Header("Camera info")]
    [SerializeField][Tooltip("Is this object visible to the camera?")]
    private bool amIVisible = true;

    

    private void OnEnable() => controls.Gameplay.Enable();
    private void OnDisable() => controls.Gameplay.Disable();

    void Awake()
    {
        lookingInput = new Vector3();
        controls = new InputBarbarian();
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        myCamera.GetComponent<SmoothCameraMovement>().AddPlayer(gameObject);
    }


    public virtual void Start()
    {        
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        GetCurrentFloor();
    }
    public virtual void Update()
    {
        Look();
             
    }
    public virtual void FixedUpdate()
    {
        Move();
        jumpInput = controls.Gameplay.Jump.triggered;
        if (isGrounded && jumpInput)
            Jump();
        if (!isGrounded && currentFloor != null)
            rb.AddForce(-currentFloor.transform.up * ownGravity);
    }

    /*  <Visibility>*/
    private void OnBecameInvisible() => amIVisible = false;
    private void OnBecameVisible() => amIVisible = true;


    /*  <Look()>*/

    public void Look()
    {
        if (gamepadSelected) keyboardMouseSelected = false;
        if (keyboardMouseSelected) gamepadSelected = false;
        if (!gamepadSelected) keyboardMouseSelected = true;
        if (!keyboardMouseSelected) gamepadSelected = true;

        Vector2 lookValue;
        var gamepad = Gamepad.current; 
        if (gamepad != null && gamepadSelected && !keyboardMouseSelected)
        {
            
            lookValue = gamepad.rightStick.ReadValue();

            if((lookValue.x >= lookingThreshold || lookValue.y >= lookingThreshold) || (lookValue.x <= -lookingThreshold || lookValue.y <= -lookingThreshold))
                lookingInput = new Vector3(lookValue.x, 0, lookValue.y);
            rot = Quaternion.LookRotation(lookingInput);
            current = transform.rotation;

            
            transform.localRotation = Quaternion.Lerp(current, rot, sensitivity * Time.fixedDeltaTime);

            

        }
        else
        {
            
            if(keyboardMouseSelected)
            {
                Ray cameraRay = myCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

                if (groundPlane.Raycast(cameraRay, out float rayLength))
                {
                    pointToLook = cameraRay.GetPoint(rayLength);
                    Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                }
                else
                {
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                }
                Debug.Log("M&Kbrd used");
            }
        }


    }

    /* <Move()>*/
    public void Move()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null && (gamepadSelected || !keyboardMouseSelected))
        {
            Vector2 movementInput = gamepad.leftStick.ReadValue();
            hori = movementInput.x;
            verti = movementInput.y;
        }
        if (gamepad == null || (!gamepadSelected || keyboardMouseSelected))
        {
            Vector2 movementInput = controls.Gameplay.Movement.ReadValue<Vector2>();
            hori = movementInput.x;
            verti = movementInput.y;
        }

        addVel = new Vector2(hori, verti) * speed;

        if ((addVel.x >= movingThreshold || addVel.y >= movingThreshold) || (addVel.x <= -movingThreshold || addVel.y <= -movingThreshold))
            rb.MovePosition(rb.position + new Vector3(addVel.x, 0, addVel.y) * speed * Time.fixedDeltaTime);


    }

    /* <Jump()>*/
    public void Jump()
    {
        isGrounded = false;
        rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        
    }

    /* <GetCurrentFloor()>*/
    void GetCurrentFloor() //get this shit activable and desactivable
    {
        if(wantJumpinNormal)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * Mathf.Infinity, Color.cyan);
                currentFloor = hit.transform.gameObject;
            }
        }
        
    }
    
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==8)
        {
            isGrounded = true;
            GetCurrentFloor();
        }
    }



}