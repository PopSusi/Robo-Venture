using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class StunAbility : Ability
{
	private bool active;
	public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
		unlocked = true;
        player = this.gameObject;
        input = player.GetComponent<PlayerInput>();
        characterController = player.GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
		
        input.actions["StunThrow"].started+=StunStarted;
		input.actions["StunThrow"].canceled+= StunRelease;

		
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
			//DRAW THE VISUALIZATION LINE
			Debug.Log("held");
		}
    }
	private void StunStarted(InputAction.CallbackContext context){
		if(canAbility){
			active = true;
			Debug.Log("clicked");
		}
	}
	private void StunRelease(InputAction.CallbackContext context){
		if(canAbility){
			CooldownManager.CDMInstance.CooldownMaskStart(mySprite);
			active = false;
			Instantiate(projectile, player.transform.position, Quaternion.identity);
			Debug.Log("thrown");
			canAbility = false;
			StartCooldown();
		}
	}
}
