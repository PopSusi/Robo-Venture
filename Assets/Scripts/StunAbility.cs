using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class StunAbility : Ability
{
	private bool active;
  [field: Header("Ability Sub-Class")]
	[SerializeField] Transform throwPoint;
	[SerializeField] GameObject projectilePrefab;
	GameObject projectile;
	Rigidbody projectilRb;
	[SerializeField] float throwForce;
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
			//Debug.Log("held");
		}
    }
	private void StunStarted(InputAction.CallbackContext context){
		if(canAbility){
			active = true;
			Debug.Log("clicked");
           
            //projectilRb.isKinematic = true;
        }
	}
	private void StunRelease(InputAction.CallbackContext context){
		if(canAbility){
			CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
			active = false;
            projectile = Instantiate(projectilePrefab, throwPoint);
            projectilRb = projectile.GetComponent<Rigidbody>();
            projectilRb.isKinematic = false;
			projectilRb.AddForce(throwForce * Camera.main.transform.forward, ForceMode.Impulse);
            Debug.Log("thrown");
			canAbility = false;
			StartCooldown();
		}
	}
}
