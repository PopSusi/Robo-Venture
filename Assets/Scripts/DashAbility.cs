using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : Ability
{
    [SerializeField]
    float dashDistance,dashTimeToComplete;
    float distanceTraveled;
    bool isDashing;
    Vector2 dashDir;
    private void Awake()
    {
        Initialize();
        input.actions["Dash"].performed+=OnDash;
        moveAction=input.actions["Move"];
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
           
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (canAbility)
        {
           
            print(playerController.moveDir);
            dashDir = playerController.moveDir;
            if (dashDir.magnitude > 0)
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(dashDir.x, 0, dashDir.y), Vector3.up);

            }
          
            StartCoroutine(Dashing());
            //characterController.Move(this.transform.forward * dashDistance* (Time.fixedDeltaTime));
           
        }

    }

    IEnumerator Dashing()
    {
        GetComponent<Animator>().SetBool("Dash", true);
        distanceTraveled = 0;
        while (distanceTraveled < dashDistance)
        {
            //Vector3 pos=characterController.transform.position;
            characterController.Move(this.transform.forward * (dashDistance / dashTimeToComplete) * (Time.fixedDeltaTime));
            distanceTraveled += (dashDistance / dashTimeToComplete) * (Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        isDashing = false;
        GetComponent<Animator>().SetBool("Dash", false);
        canAbility = false;
        StartCooldown();
    }
}