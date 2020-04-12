using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : MonoBehaviour

{
    //--------------------SCRIPTABLEOBJECTS--------------------------//
    public CharacterClass characterClass; 
    

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
    private Vector2 addVel;
    float hori, verti;
    //-----------------------LOOKING-------------------------------//
    [Header("Looking")]
    private float sensitivity = 120f;
    private float lookingThreshold = 0.05f;
    private Quaternion rot;
    private Quaternion current;
    private Vector3 mouseLookInput;
    
    //-------------------INPUTS-----------------------------------//
    [Header("Inputs")]

    [SerializeField] private int playerIndex = 0; 
    public Vector2 movementInput;
    public Vector2 lookInput;
    public float attackInput;
    public float ability1Input;
    public float ability2Input;
    public float ability3Input;
    public float ability4Input;

    //------------------------CAMERA------------------------------//
    [Header("Camera info")]
    [SerializeField][Tooltip("Is this object visible to the camera?")]
    private bool amIVisible = true;
   




    void Awake()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        myCamera.GetComponent<SmoothCameraMovement>().AddPlayer(gameObject);
    }

    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    public virtual void Start() { }
    public virtual void Update()
    {
        Look();
    }
    public virtual void FixedUpdate()
    {
       Move();
    }

    /* <InputsSetters>*/
    public void SetMoveInputVector(Vector2 move) => movementInput = move;
    public void SetLookInputVector(Vector2 look) => lookInput = look;
    public void SetAttackInputVector(float attack) => attackInput = attack;
    public void SetAbility1InputVector(float ability1) => ability1Input = ability1;
    public void SetAbility2InputVector(float ability2) => ability2Input = ability2;
    public void SetAbility3InputVector(float ability3) => ability3Input = ability3;
    public void SetAbility4InputVector(float ability4) => ability4Input = ability4;
    public int GetPlayerIndex() { return playerIndex; }



    /*  <Visibility>*/
    private void OnBecameInvisible() => amIVisible = false;
    private void OnBecameVisible() => amIVisible = true;


    /*  <Look()>*/
    public void Look()
    {   
        
        if ((lookInput.x >= lookingThreshold || lookInput.y >= lookingThreshold) || (lookInput.x <= -lookingThreshold || lookInput.y <= -lookingThreshold))
        {
           
                rot = Quaternion.LookRotation(new Vector3(lookInput.x, 0, lookInput.y));
                transform.rotation = Quaternion.Lerp(current, rot, sensitivity * Time.fixedDeltaTime);
                current = transform.rotation; 
        }
        else
        {
            transform.rotation = current;
        }
        
    }

    /* <Move()>*/
    public void Move()
    {
        addVel = new Vector2(movementInput.x, movementInput.y) * speed;

        if ((addVel.x >= movingThreshold || addVel.y >= movingThreshold) || (addVel.x <= -movingThreshold || addVel.y <= -movingThreshold))
            rb.MovePosition(rb.position + new Vector3(addVel.x, 0, addVel.y) * Time.fixedDeltaTime);


    }
    /* <Attack>*/
    public virtual void Attack() { }

}