using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchAbility : Ability
{
    public int punchIndex;
    public GameObject[] hitboxes;
    public Vector3[] offset;
    private bool canPunch = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<ThirdPersonPlayerController>();
        characterController = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        input.actions["Punch"].performed+=OnPunch;
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if(canPunch){
            canPunch = false;
            punchIndex += 1;
            punchIndex = punchIndex == 4 ? 1 : punchIndex; 
            Instantiate(hitboxes[punchIndex - 1], transform.position + offset[punchIndex -1], Quaternion.identity);
            anim.Play("Punch");
            StartCoroutine("PunchDelay");
        }

    }
    IEnumerator PunchDelay(){
        yield return new WaitForSeconds(.15f);
        canPunch = true;
    }
}
