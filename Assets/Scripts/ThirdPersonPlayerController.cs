using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : Damageable
{

    CharacterController controller;
    PlayerInput input;
    public InputAction moveAction { get;private set; }
    Camera cam;
    //Vector2 targetVelocity;
    Vector2 moveVelocity;
    float ySpeed;
    [SerializeField]
    const float gravity=-20f;
    [SerializeField]
    CinemachineFreeLook cinemachineFreeLook;
    [SerializeField]
    float speed, accel, airAccel,jumpForce;
    float punchDistanceTraveled;
    public float Speed { get { return speed; } }
    public float Accel { get { return accel; } }
    public float AirAccel { get { return airAccel; } }
    public float JumpForce { get { return jumpForce; } }
    public float Gravity { get { return gravity; } }
    Animator anim;
    public Vector2 moveDir { get; private set; }

    PlayerMovementState playerMovement;
    //bool 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        //cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
        input.actions["Jump"].performed+=OnJump;
        input.actions["Punch"].performed+=OnPunch;
        moveAction.performed += OnMove;

        playerMovement = new PlayerMovementState(moveAction, controller, this.transform,accel,airAccel,gravity);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        cinemachineFreeLook.GetComponent<CinemachineInputProvider>().PlayerIndex = input.playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {
            Punching();
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
    void Punching()
    {
        controller.Move(this.transform.forward * 3 *10 * (Time.fixedDeltaTime));
        punchDistanceTraveled += 3 * 10 * (Time.fixedDeltaTime);
        if (punchDistanceTraveled >= 3)
        {
            anim.Play("Grounded");
            punchDistanceTraveled = 0;
        }
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {
            return;
        }
        anim.Play("Punch");
        RaycastHit hit;
        if(Physics.SphereCast(this.transform.position,.5f,new Vector3(moveDir.x,0, moveDir.y),out hit,5))
        {

        }
        else
        {

        }

    }

}
