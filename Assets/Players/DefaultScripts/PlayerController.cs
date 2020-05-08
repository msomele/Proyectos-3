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
    [HideInInspector] public GameObject myCamera;
    [HideInInspector] public Rigidbody rb;
    public float hp = 140;

    //-----------------------MOVEMENT-------------------------------//
    [Header("Movement")]
    [Tooltip("Speed assigned 2 the player")]
    [HideInInspector] public float speed;
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
    public InputHolders inputH;
    /*
    public Vector2 movementInput;
    public Vector2 lookInput;
    public float attackInput;
    public float ability1Input;
    public float ability2Input;
    public float ability3Input;
    public float ability4Input;
    */

    //------------------------CAMERA------------------------------//
    [Header("Camera info")]
    [SerializeField][Tooltip("Is this object visible to the camera?")]
    private bool amIVisible = true;
   




    void Awake()
    {
        
    }
    

    public virtual void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        myCamera.GetComponent<SmoothCameraMovement>().AddPlayer(gameObject);
        inputH = this.GetComponentInChildren<InputHolders>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
    public virtual void Update()
    {
        Look();
    }
    public virtual void FixedUpdate()
    {
       Move();
    }

    /* <InputsSetters>*/

    public int GetPlayerIndex() { return playerIndex; }
    /*
    public void SetMoveInputVector(Vector2 move) => movementInput = move;
    public void SetLookInputVector(Vector2 look) => lookInput = look;
    public void SetAttackInputVector(float attack) => attackInput = attack;
    public void SetAbility1InputVector(float ability1) => ability1Input = ability1;
    public void SetAbility2InputVector(float ability2) => ability2Input = ability2;
    public void SetAbility3InputVector(float ability3) => ability3Input = ability3;
    public void SetAbility4InputVector(float ability4) => ability4Input = ability4;
    */

    /*  <Visibility>*/
    private void OnBecameInvisible() => amIVisible = false;
    private void OnBecameVisible() => amIVisible = true;


    /*  <Look()>*/
    public void Look()
    {   
        
        if ((inputH.lookInput.x >= lookingThreshold || inputH.lookInput.y >= lookingThreshold) || (inputH.lookInput.x <= -lookingThreshold || inputH.lookInput.y <= -lookingThreshold))
        {
           
                rot = Quaternion.LookRotation(new Vector3(inputH.lookInput.x, 0, inputH.lookInput.y));
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
        addVel = new Vector2(inputH.movementInput.x, inputH.movementInput.y) * speed;

        if ((addVel.x >= movingThreshold || addVel.y >= movingThreshold) || (addVel.x <= -movingThreshold || addVel.y <= -movingThreshold))
            rb.MovePosition(rb.position + new Vector3(addVel.x, 0, addVel.y) * Time.fixedDeltaTime);


    }
    /* <Attack>*/
    public virtual void Attack() { }

}