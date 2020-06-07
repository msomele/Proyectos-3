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
    [HideInInspector] public GameObject rotation;

    //public GameObject pauseMenu;
    public bool isGamePaused;

    public float hp = 350;

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
    public GameObject pointer; 
    private float sensitivity = 120f;
    private float lookingThreshold = 0.05f;
    private Quaternion rot;
    private Quaternion current;
    private Vector3 mouseLookInput;
    private bool canMove;
    [HideInInspector] public bool doubleAttack;
    [HideInInspector] public Quaternion previousRotation;
    Quaternion curr;
    //-------------------INPUTS-----------------------------------//
    [Header("Inputs")]
    public Sprite[] playersSprites = new Sprite[2];
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
        canMove = true;
        isGamePaused = false;
    }
    

    public virtual void Start()
    {
        doubleAttack = false;
        rotation = GetComponentInChildren<RotationPlayer>().gameObject;
        curr = rotation.transform.rotation;
        pointer = GetComponentInChildren<Pointer>().gameObject;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        myCamera.GetComponent<SmoothCameraMovement>().AddPlayer(gameObject);
        inputH = this.GetComponentInChildren<InputHolders>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;

        this.gameObject.GetComponentInChildren<LookAtUI>().gameObject.GetComponent<SpriteRenderer>().sprite = playersSprites[playerIndex];

      

    }

    
    public virtual void Update()
    {
        Look();
        if (inputH.pauseInput != 0)
        {
            PauseGame();
            Debug.LogWarning("Start pressed");
        }

        inputH.pauseInput = 0;

        


    }
    public virtual void FixedUpdate()
    {

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
                pointer.transform.rotation = Quaternion.Lerp(current, rot, sensitivity * Time.fixedDeltaTime); //transform.rotation
                current = pointer.transform.rotation;//transform.rotation; 
        }
        else
        {
            pointer.transform.rotation = current; //transform.rotation
        }
        
    }

    /* <Move()>*/
    public virtual void Move(Animator player)
    {
        if (canMove)
        {
            addVel = new Vector2(inputH.movementInput.x, inputH.movementInput.y) * speed;

            if ((addVel.x >= movingThreshold || addVel.y >= movingThreshold) || (addVel.x <= -movingThreshold || addVel.y <= -movingThreshold))
            {
                rb.MovePosition(rb.position + new Vector3(addVel.x, 0, addVel.y) * Time.fixedDeltaTime);
                rb.gameObject.transform.LookAt(rb.position + new Vector3(addVel.x, 0, addVel.y) * Time.fixedDeltaTime);
                player.SetBool("Running", true);
            }
            else
                player.SetBool("Running", false);

        }


    }
    /* <Attack>*/
    public virtual void Attack() //da problemas el previousRotation con el doble aa ... solucion?? npi
    {
        RotateTowardsPointer();
    }
    public void RotateTowardsPointer()
    {
        canMove = false;
        Vector3 dir = pointer.GetComponentInChildren<PointerDirection>().transform.position;
        Quaternion auxrot = Quaternion.LookRotation(dir - rotation.transform.position);
        rotation.transform.rotation = Quaternion.Lerp(curr, auxrot, 1f);
        curr = rotation.transform.rotation;

    }

    public void SaveRotation()
    {
        previousRotation = rotation.transform.rotation;
    }



    public IEnumerator NowCanMove(Animator player)//,float time) //call it at the end of Attack funcion in each playerScript
    {
        //yield return new WaitForSeconds(time); 
        yield return null;
        rotation.transform.rotation = previousRotation;
        canMove = true;
    }

    /* <Pause> */
    public void PauseGame()
    {
        if (!isGamePaused)
        {Camera.main.GetComponentInChildren<PostProcessingRealtimeChanger>().ChangeFov(0);
            isGamePaused = true;
            Time.timeScale = 0f;
            myCamera.GetComponent<MainMenuLogic>().BackToMainMenu();
        }
        else 
        {
            isGamePaused = false;
            Time.timeScale = 1f;
            Camera.main.GetComponentInChildren<PostProcessingRealtimeChanger>().ChangeFov(1);
            myCamera.GetComponent<MainMenuLogic>().ResumeGame();
        }
    }

    public int _playerIndex
    {
        get{ return playerIndex; }
        set{ playerIndex = value; }
    }
    



}