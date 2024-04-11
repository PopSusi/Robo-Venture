using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : MonoBehaviour, Damageable
{
    [field: Header("Damageable Variables")] //Damageable Variables
    public float HP { get; set; } = 6f;
	private float maxHP;
    [Tooltip("Time before being able to be damaged again.")]
    public float damageDelay { get; set; } = 1.5f;
    [Tooltip("If player can take damage.")]
    public bool vulnerable { get; set; } = true;
    //Controller Components
    CharacterController controller;
    PlayerInput input;
    private InputAction moveAction;
    Camera cam;
    PlayerMovementState playerMovement;
    Animator anim;
    RoboLevels myGM;
    private UIManager UIman;

    //Move vars
    //Vector2 targetVelocity;
    Vector2 moveVelocity;
    [Tooltip("Camera.")]
    [SerializeField]
    CinemachineFreeLook cinemachineFreeLook;
	[field: Header("Movement Variables")] [SerializeField] float speed;
    [SerializeField]
    float accel, airAccel,jumpForce,turnSpeed,coyoteTimeMax;
    float coyoteTime;
    bool hasJumped;
    public float Speed { get { return speed; } }
    public float Accel { get { return accel; } }
    public float AirAccel { get { return airAccel; } }
    public float JumpForce { get { return jumpForce; } }
    public float Gravity { get { return gravity; } }
    float ySpeed;
    [SerializeField]
    const float gravity=-20f;
    public Vector2 moveDir { get; private set; }

    [field: Header("Audio Related")]//Audio
    [SerializeField]
    [Tooltip("Source for footsteps.")]
    private AudioSource footSource;
    [Tooltip("Source for other SFX.")]
    [SerializeField]
    private AudioSource sptnsSource;
    [SerializeField]
    private AudioClip hit, hitVariant, footSteps, lose;
    bool footPaused = true;
    [field: Header("Options Related")]//Options
    [Tooltip("Enable abilities on startup.")]
    bool invincible, moreDamage;
    public static ThirdPersonPlayerController instance;
    public int fuelCellsInserted;
    public int fuelCellsTotal;
	
    //Establish Singleton
	private void Awake(){
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        maxHP = HP;
	}
    //Grab other Singletons, set up character variables based on selected cheats, and disable death UI
    private void Start()
    {
        
        Initialize();
        OptionsInitialize();
        UIman.warningUI.gameObject.SetActive(false);
    }

    private void Initialize() //Variables and Components
    {
        //Component Gets
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        //cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        cam = Camera.main;
        myGM = RoboLevels.instance;
        UIman = UIManager.instance;

        //Move Inputs
        moveAction = input.actions["Move"];
        moveAction.performed += OnMove;
        //playerMovement = new PlayerMovementState(moveAction, controller, this.transform,accel,airAccel,gravity);

        //Other Inputs
        input.actions["Jump"].performed += OnJump;
        input.actions["DebugDamage"].performed += DebugDamage;
    }

    public void OptionsInitialize()
    {
        invincible = Settings.InfiniteHealth;
        moreDamage = Settings.HardMode;

        //Enable one by one
        GetComponent<DashAbility>().unlocked = Settings.dash;
        GetComponent<GrappleAbility>().unlocked = Settings.grapple;
        GetComponent<WallAbility>().unlocked = Settings.wall;
    }

    //Reenable activated abilities
    private void SetAbility(string ability) //dash, grapple, wall
    {
        switch(ability)
        {
            case "dash":
                GetComponent<DashAbility>().unlocked = true;
                break;
            case "grapple":
                GetComponent<GrappleAbility>().unlocked = true;
                break;
            case "wall":
                GetComponent<WallAbility>().unlocked = true;
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        //cinemachineFreeLook.GetComponent<CinemachineInputProvider>().PlayerIndex = input.playerIndex;
    }

    //Play footsteps, animations, and set Angles
    private void FixedUpdate()
    {
        footSource.Pause();
        footPaused = true;
        
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
        anim.SetFloat("Speed", controller.velocity.magnitude);
        if(controller.velocity.magnitude < 1)
        {
            footSource.Pause();
            footPaused = true;
        }
        if(footPaused && controller.velocity.magnitude > 1)
        {
            footSource.Play();
            footPaused = false;
        }
    }
    
    void Movement(Vector2 targetVelocity)
    {
        moveVelocity = Vector2.MoveTowards(new Vector2(moveVelocity.x, moveVelocity.y), targetVelocity, (controller.isGrounded ? accel : airAccel));

        controller.Move(new Vector3(moveVelocity.x, ySpeed, moveVelocity.y) * Time.fixedDeltaTime);
        if (moveAction.IsPressed())
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveVelocity.x, 0, moveVelocity.y), Vector3.up);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, turnSpeed*Time.fixedDeltaTime);
        }
        if (!controller.isGrounded)
        {
            coyoteTime += Time.fixedDeltaTime;
            ySpeed += gravity * Time.fixedDeltaTime;
            anim.SetBool("isFalling", true);
        }
        else
        {
            coyoteTime = 0;
            anim.SetBool("isFalling", false);
            ySpeed = -0.1f;
            hasJumped = false;
        }

    }
    void DebugDamage(InputAction.CallbackContext context)
    {
        TakeDamage(1f);
    }
    void OnMove(InputAction.CallbackContext context)
    {
    }
    void OnJump(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Jump");
        if (controller.isGrounded||(coyoteTime< coyoteTimeMax&&!hasJumped))
        {
            //print("should jump");
            //coyoteTime = 0;
            ySpeed = Mathf.Sqrt(jumpForce * -3.0f * gravity);
            //isGrounded = false;
            hasJumped = true;
        }
    }
    
    void KnockBack(Vector3 force)
    {
       controller.Move(force * Time.fixedDeltaTime);
    }
    
    public void Die(){
        //gameObject.GetComponent<CharacterController>().enabled = false;
        UIman.Death();
        Debug.Log("Death");
        sptnsSource.clip = lose;
        sptnsSource.Play();
    }
    public void TakeDamage(float damage)
    {
        if (moreDamage)
        {
            damage++;
        }
        if (vulnerable && !invincible)
        {
            StopCoroutine("RegenDelay");
            HP -= damage;
            if(!(HP <= 0)){//Not at zero
                vulnerable = false;
                GetComponent<AudioSource>().Play();
                StartCoroutine("DamageDelay");
                StartCoroutine("BeginRegenDelay");
                UIman.HealthbarUpdate(HP);
            } else {
                Die();
            }
        }
    }
    private IEnumerator BeginRegenDelay()
    {
        yield return new WaitForSeconds(5f);
        TakeDamage(-1f);
        if(HP != maxHP)
        {
            StartCoroutine("RegenDelay");
        }
    }
    private IEnumerator RegenDelay()
    {
        yield return new WaitForSeconds(2f);
        TakeDamage(-1f);
        if (HP != maxHP)
        {
            StartCoroutine("RegenDelay");
        }
    }
    IEnumerator DamageDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(damageDelay);
        yield return wait;
        vulnerable = true;
    }

    public void EnableDash()
    {
        Settings.dash = true;
        gameObject.GetComponent<DashAbility>().unlocked = true;
    }
    public void EnableGrapple()
    {
        Settings.grapple = true;
        gameObject.GetComponent<GrappleAbility>().unlocked = true;
    }
    public void EnableWall()
    {
        Settings.wall = true;
        gameObject.GetComponent<WallAbility>().unlocked = true;
    }
    public void PlaySound(AudioClip clip)
    {
        sptnsSource.clip = clip;
        sptnsSource.Play();
    }
    public void InsertFuelCell()
    {
        fuelCellsInserted++;
        if (fuelCellsInserted >= 4)
        {
            //WIN GAME
        }
    }
}