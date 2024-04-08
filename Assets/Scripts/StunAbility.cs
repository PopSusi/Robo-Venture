using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;


public class StunAbility : Ability
{
	private bool active;
  [field: Header("Ability Sub-Class")]
    [Tooltip("Where the grenade it thrown from.")][SerializeField] Transform throwPoint;
	[SerializeField] GameObject projectilePrefab;
	GameObject projectile;
	Rigidbody projectilRb;
	[SerializeField] float throwForce;
    Camera cam;
    LineRenderer lineRenderer;
    [SerializeField]
    int segmentCount=50;
    Vector3[] segments;
    float curveLength = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer=GetComponent<LineRenderer>();
        segments = new Vector3[segmentCount];
        lineRenderer.positionCount=segmentCount;
        anim = GetComponent<Animator>();
        cam= Camera.main;
		unlocked = true;
        player = this.gameObject;
        input = player.GetComponent<PlayerInput>();
        characterController = player.GetComponent<CharacterController>();
        moveAction = input.actions["Move"];
		
        input.actions["StunThrow"].performed+=StunStarted;
		input.actions["StunThrow"].canceled+= StunRelease;
        lineRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            //DRAW THE VISUALIZATION LINE
            //Debug.Log("held");
            //this.transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.y), Vector3.up);
            TrajectoryLine();
        }
        else
        {
            
        }
    }

    void TrajectoryLine()
    {
        Vector3 startPos=throwPoint.position;
        segments[0] = startPos;
        lineRenderer.SetPosition(0, startPos);
        Vector3 startVelocity = throwForce * cam.transform.forward;
        for(int i = 1; i < segmentCount; i++)
        {
            float timeOffset = (i * Time.fixedDeltaTime * curveLength);
            Vector3 gravityOffset = 0.5f * Physics.gravity * Mathf.Pow(timeOffset, 2);
            segments[i] = segments[0] + startVelocity * timeOffset + gravityOffset;
            lineRenderer.SetPosition(i, segments[i]);
        }
    }
	private void StunStarted(InputAction.CallbackContext context){
		if(canAbility){
			active = true;
            Debug.Log("clicked");
            anim.SetBool("GrenadeHold", true);
            //projectilRb.isKinematic = true;
            lineRenderer.enabled = true;
        }
	}
	private void StunRelease(InputAction.CallbackContext context){
		if(canAbility){
            anim.SetBool("GrenadeHold", false);
            CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
			active = false;
            projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
            projectilRb = projectile.GetComponent<Rigidbody>();
			projectilRb.AddForce(throwForce * cam.transform.forward, ForceMode.Impulse);
            //Debug.Log("thrown");
			canAbility = false;
			StartCooldown();
            lineRenderer.enabled = false;
        }
	}
}
