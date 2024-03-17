using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : Ability
{
    
    [SerializeField]
    float dashDistance,dashSpeed;
    bool isDashing;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        input = player.GetComponent<PlayerInput>();
        characterController = player.GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
        input.actions["Dash"].performed+=OnDash;
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
		if(canAbility){
        	Vector3 moveDir = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        	characterController.Move(this.transform.forward*dashDistance*(Time.fixedDeltaTime));
        	canAbility = false;
			StartCooldown();
		}

    }
}
