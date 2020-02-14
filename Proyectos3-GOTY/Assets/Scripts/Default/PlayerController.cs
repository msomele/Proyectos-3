using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour 

{
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
    public bool isGrounded; 
    //-------------------HABILITIES-------------------------------//
    private InputBarbarian controls; 
    [HideInInspector]
    public bool hab1 = false; 
    [HideInInspector]
    public bool hab2 = false; 
    [HideInInspector]
    public bool hab3 = false; 
    [HideInInspector]
    public bool hab4 = false;

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
        myCamera.GetComponent<SmoothCameraMovement>().AddPlayer(gameObject);
    }


    public void Start()
    {        
        rb = rb.GetComponent<Rigidbody>();
        


        Cursor.visible = false; 

    }
    public void Update()
    {
        Look();
             
    }
    public void FixedUpdate()
    {
       Move();
    }
    

    private void OnBecameInvisible() => amIVisible = false;
    private void OnBecameVisible() => amIVisible = true;


    /*  <Look()>
    Crea un plano en +y, lanza un raycast desde la cam a dicho plano en función del puntero.El jugador rota hacia la dirección de dicho corte
      en el caso de que el ratón se salga del plano, el jugador se quedará mirando al último punto.
    */

    public void Look()
    {
        Vector2 lookValue = controls.Gameplay.Look.ReadValue<Vector2>();

        lookingInput.y += lookValue.x  * sensitivity * Time.deltaTime; //moves player in y axis based
                                                                       //on the x axis of the right joystick (or the mouse horizontal movement)
        lookingInput.y = Mathf.Clamp(lookingInput.y, 0, 360);
        if (lookingInput.y == 360) lookingInput.y = 0; else if (lookingInput.y == 0) lookingInput.y = 360; //clamping and setting rotations for preventing maxbounds in vector
        
        //maybe clamping as well lookValue.y so it goes 90º each side input:::? 
        transform.rotation = Quaternion.Euler(lookingInput);


        /* Old method with old mouse settings 
        Ray cameraRay = myCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); 
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        else
        {
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }  */
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

    
    public void JumpEv()
    {
        if(isGrounded)
        rb.AddForce(transform.up, ForceMode.VelocityChange);

    }

  

    

}