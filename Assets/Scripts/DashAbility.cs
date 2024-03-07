using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{
    PlayerInput input;
    CharacterController characterController;
    [SerializeField]
    float dashDistance,dashSpeed;
    InputAction moveAction;
    bool isDashing;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        input.actions["Dash"].performed+=OnDash;
        moveAction=input.actions["Move"];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {

        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        Vector3 moveDir = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        characterController.Move(this.transform.forward*dashDistance*(Time.fixedDeltaTime));
        isDashing = true;


    }
}
