using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 

{
//-----------------------ASSIGNABLES-----------------------------//
    [Header("Assignables")]
    [Tooltip("Camera assigned 2 the player")]
    public GameObject myCamera;
    [Tooltip("Players rigidbody")]
    public Rigidbody rb;
    [Tooltip("Layer Mask acting as floor, used for the looking")]
    public LayerMask FloorLayer;
    [HideInInspector]
    public float hori, verti;
    private Vector3 normalVector = Vector3.up;

//-----------------------MOVEMENT-------------------------------//
    [Header("Movement")]
    [Tooltip("Speed assigned 2 the player")]
    public float speed;
    [Tooltip("Maximum speed assigned 2 the player")]
    public float maxSpeed = 20; 
    [Tooltip("Maximum slope angle")]
    public float maxSlopeAngle = 35f; 
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 pointToLook;

//--------------------JUMPING----------------------------------//
    [Header("Jumping")]
    [Tooltip("Jump force assigned 2 the player")]
    public float jumpForce;
    [Tooltip("Cooldown for players jump")]
    public float jumpCooldown = 2f;
    [Tooltip("Is the player jumping?")] 
    public bool isJumping;
    [Tooltip("Is the player grounded?")]
    public bool isGrounded;
    [Tooltip("Is the player ready to jump?")]
    public bool isReadyToJump = true;
//-------------------------------------------------------------//

    public void Start()
    {        
        rb = rb.GetComponent<Rigidbody>();
        myCamera.GetComponent<SmoothCameraMovement>().playerTransform = gameObject.transform;
    }
    public void Update()
    {
        MyInput();
        Look();
             
    }
    public void FixedUpdate()
    {
       Move();
    }

    /*  <Look()>
    Crea un plano en +y, lanza un raycast desde la cam a dicho plano en función del puntero.El jugador rota hacia la dirección de dicho corte
      en el caso de que el ratón se salga del plano, el jugador se quedará mirando al último punto.
    */

    public void Look()
    {
         myCamera.GetComponent<SmoothCameraMovement>().playerTransform = transform;
        moveInput = new Vector3(hori, 0f, verti);
        
        moveVelocity = moveInput * speed;
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
        }  
    }

    /* <Move()>
    Hay que cambiarlo, ahora se mueve con velocity y lo ideal es que se moviera con fuerzas. Permite que el player se mueva y condiciona el salto.
    */

    public void Move()
    {
         rb.AddForce(Vector3.down * 10);
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x;
        float yMag = mag.y;
        
        //counter movement here

        if (isReadyToJump && isJumping)
            Jump();
        
        float maxSpeed = this.maxSpeed;
        
        if(hori > 0 && xMag > maxSpeed) hori = 0;
        if(hori < 0 && xMag < -maxSpeed) hori = 0;
        if(verti > 0 && yMag > maxSpeed) verti = 0;
        if(verti < 0 && yMag < -maxSpeed) verti = 0; 
        
        float multiplier = 1f, multiplierV = 1f; 

        //moverse la mitad de rápido en el aire
        if(!isGrounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }
        rb.AddForce(rb.transform.forward * verti * speed * Time.deltaTime * multiplierV * multiplier);
        rb.AddForce(rb.transform.right * hori * speed * Time.deltaTime * multiplier);

    }
    

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = rb.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle,moveAngle);
        float v = 90 - u;
        float magnitude = rb.velocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag,yMag);
    }
    /* <MyInput()>
    Simplemente guarda el input necesario por ahora; ampliable.
    .*/
    public void MyInput()
    {
        isJumping = Input.GetButton("Jump");
        hori = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");
    }
    /* <Jump()>
    Todo lo necesario para que salte y prepare el salto rápidamente. 
    */
    private void Jump()
    {
        if(isGrounded && isReadyToJump)
        {
            isReadyToJump = false;
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            Vector3 velocity = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(velocity.x, 0, velocity.z);
            else if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
            }
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        isReadyToJump = true; 
    }
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }
    private void StopGrounded()
    {
        isGrounded = false; 
    }

    private bool CancellingGrounded;

    private void OnCollisionStay(Collision col)
    {
        int layer = col.gameObject.layer;
        if (FloorLayer != (FloorLayer | (1 << layer))) return;

        for (int i = 0; i < col.contactCount; i++)
        {
            Vector3 normal = col.contacts[i].normal;
            if(IsFloor(normal))
            {
                isGrounded = true;
                CancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        if(!CancellingGrounded)
        {
            CancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * 3f);
        }
    }







}