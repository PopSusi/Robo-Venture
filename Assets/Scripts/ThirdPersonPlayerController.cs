using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : Damageable
{
    //Controller Components
    CharacterController controller;
    PlayerInput input;
    public InputAction moveAction { get;private set; }
    Camera cam;
    PlayerMovementState playerMovement;
    Animator anim;
    RoboLevels myGM;
    public UIManager UIman;

    //Move vars
                //Vector2 targetVelocity;
    Vector2 moveVelocity;
    [SerializeField]
    CinemachineFreeLook cinemachineFreeLook;
    [SerializeField]
    float speed, accel, airAccel,jumpForce;
    public float Speed { get { return speed; } }
    public float Accel { get { return accel; } }
    public float AirAccel { get { return airAccel; } }
    public float JumpForce { get { return jumpForce; } }
    public float Gravity { get { return gravity; } }
    float ySpeed;
    [SerializeField]
    const float gravity=-20f;

    //Punches
    float punchDistanceTraveled;
    public Vector2 moveDir { get; private set; }
    //public int punchIndex;
    //public GameObject[] hitboxes;
    //public Vector3[] offset;
    //private bool canPunch;

    //Options
    public static bool dash, wall, grapple;
    bool invincible;
    private void Awake()
    {
        Initialize();
        OptionsInitialize();
    }

    private void Initialize() //Variables and Components
    {
        //Component Gets
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        //cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        cam = Camera.main;
        myGM = GameObject.FindWithTag("LevelGM").GetComponent<RoboLevels>();
        UIman = myGM.GetComponent<UIManager>();

        //Move Inputs
        moveAction = input.actions["Move"];
        moveAction.performed += OnMove;
        //playerMovement = new PlayerMovementState(moveAction, controller, this.transform,accel,airAccel,gravity);

        //Other Inputs
        input.actions["Jump"].performed+=OnJump;

        //Need to switch and switch back to set Pause for UI and Player maps
        input.actions["Pause"].performed+=OnPause;
        input.SwitchCurrentActionMap("UI");
        input.actions["Pause"].performed+=OnPause;
        input.SwitchCurrentActionMap("Player");
    }

    public void OptionsInitialize()
    {
        invincible = UIman.infiniteHealth;
        if(UIman.allModChips){ //First check for All
            dash = true;
            grapple = true;
            wall = true;
        } else {
            //NEED TO FUNCTIONALITY TO READ SAVE FILE AND SEE WHATS ACTIVE
        }
        //Enable one by one
        GetComponent<DashAbility>().enabled = dash;
        GetComponent<GrappleAbility>().enabled = grapple;
        GetComponent<WallAbility>().enabled = wall;
    }

    private void SetAbility(string ability) //dash, grapple, wall
    {
        switch(ability)
        {
            case "dash":
                GetComponent<DashAbility>().enabled = true;
                break;
            case "grapple":
                GetComponent<GrappleAbility>().enabled = true;
                break;
            case "wall":
                GetComponent<WallAbility>().enabled = true;
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        cinemachineFreeLook.GetComponent<CinemachineInputProvider>().PlayerIndex = input.playerIndex;
    }

    private void FixedUpdate()
    {

        Quaternion rot = Quaternion.AngleAxis(cinemachineFreeLook.m_XAxis.Value, Vector3.forward);
        moveDir = Quaternion.Inverse(rot) * moveAction.ReadValue<Vector2>();
        Vector2 targetVelocity = moveDir * speed;
        //if (anim.GetCurrentAnimatorStateInfo(0))
        //{

        //}
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Base"))
        {
           Movement(targetVelocity);
        }


    }
    
    void Movement(Vector2 targetVelocity)
    {
        moveVelocity = Vector2.MoveTowards(new Vector2(moveVelocity.x, moveVelocity.y), targetVelocity, (controller.isGrounded ? accel : airAccel));

        controller.Move(new Vector3(moveVelocity.x, ySpeed, moveVelocity.y) * Time.fixedDeltaTime);
        if (moveAction.IsPressed())
        {
            this.transform.rotation = Quaternion.LookRotation(new Vector3(moveVelocity.x, 0, moveVelocity.y), Vector3.up);
        }
        if (!controller.isGrounded)
        {
            ySpeed += gravity * Time.fixedDeltaTime;

        }
        else
        {
            ySpeed = -0.1f;
        }

    }
    void OnMove(InputAction.CallbackContext context)
    {

    }
    void OnJump(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Jump");
        if (controller.isGrounded)
        {
            print("should jump");
            ySpeed = Mathf.Sqrt(jumpForce * -3.0f * gravity);
            //isGrounded = false;
        }
    }
    
    void KnockBack(Vector3 force)
    {
       controller.Move(force * Time.fixedDeltaTime);
    }
    public void Die(){
        Destroy(this.gameObject);
    }
    private void OnPause(InputAction.CallbackContext context){
        UIman.PauseGame();
    }
    public override void TakeDamage(float damage)
    {
        if (vulnerable && !invincible)
        {
            HP -= damage;
            if(!(HP <= 0)){//Not at zero
                vulnerable = false;
                GetComponent<AudioSource>().Play();
                StartCoroutine("DamageDelay");
            } else {
                Die();
            }
        }
    }
}