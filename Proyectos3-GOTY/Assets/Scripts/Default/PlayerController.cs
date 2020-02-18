using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector2 addVel;
    [SerializeField][Tooltip("Looking rotation euler verctor")]
    private Vector3 lookingInput;
    [Tooltip("Mouse sensitivity")]
    public float sensitivity;
    float hori, verti;

    //--------------------JUMPING----------------------------------//
    public float jumpForce;
    public float ownGravity = 10;
    public bool isGrounded;
    public bool jumpInput = false;
    [SerializeField] private GameObject currentFloor;
    RaycastHit hit;
    //-------------------INPUTS-----------------------------------//
    public InputBarbarian controls; 

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
        if (!isGrounded && currentFloor!=null)
            rb.AddForce(-currentFloor.transform.up * ownGravity);
    }

    /*  <Visibility>*/
    private void OnBecameInvisible() => amIVisible = false;
    private void OnBecameVisible() => amIVisible = true;


    /*  <Look()>*/

    public void Look()
    {
        Vector2 lookValue = controls.Gameplay.Look.ReadValue<Vector2>();
        lookingInput = new Vector3(lookValue.x, 0 , lookValue.y);

        transform.LookAt((rb.position + lookingInput));

    }

    /* <Move()>*/
    public void Move()
    {
        Vector2 movementInput = controls.Gameplay.Movement.ReadValue<Vector2>();
        hori = movementInput.x;
        verti = movementInput.y;

        addVel = new Vector2(hori, verti) * speed;
        
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * Mathf.Infinity, Color.cyan);
            currentFloor = hit.transform.gameObject;
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