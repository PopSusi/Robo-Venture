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
    InputAction moveAction;
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
    //bool 
    private void Awake()
    {
        cam = Camera.main;
        //cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
        input.actions["Jump"].performed+=OnJump;
        moveAction.performed += OnMove;
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
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        Vector2 targetVelocity =moveDir*speed;

        Quaternion rot = Quaternion.AngleAxis(cinemachineFreeLook.m_XAxis.Value, Vector3.forward);
        targetVelocity = Quaternion.Inverse(rot) * targetVelocity;
        moveVelocity = Vector2.MoveTowards(new Vector2(controller.velocity.x,controller.velocity.z), targetVelocity,(controller.isGrounded? accel:airAccel));
        
        controller.Move( new Vector3(moveVelocity.x, ySpeed, moveVelocity.y) * Time.fixedDeltaTime);
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

}
