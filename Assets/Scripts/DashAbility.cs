using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : Ability
{
    [SerializeField]
    float dashDistance,dashSpeed;
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
            //Vector3 pos=characterController.transform.position;
            characterController.Move(this.transform.forward * dashDistance*dashSpeed * (Time.fixedDeltaTime));
            distanceTraveled += dashDistance * dashSpeed * (Time.fixedDeltaTime);
            if (distanceTraveled >= dashDistance)
            {
                isDashing = false;
                GetComponent<Animator>().SetBool("Dash", false);
            }
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (canAbility)
        {
            GetComponent<Animator>().SetBool("Dash", true);
            print(playerController.moveDir);
            dashDir = playerController.moveDir;
            this.transform.rotation = Quaternion.LookRotation(new Vector3(dashDir.x, 0, dashDir.y), Vector3.up);
            distanceTraveled = 0;
            isDashing = true;
        }

    }
}