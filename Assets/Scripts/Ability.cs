using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{
    protected PlayerInput input;
    protected CharacterController characterController;
    protected ThirdPersonPlayerController playerController;
    protected InputAction moveAction;
    protected Animator anim;
    protected GameObject player;
    protected AudioSource myAudio;
    public float cooldown;
    protected bool canAbility = true;
    // Start is called before the first frame update

    protected void Initialize(){
        anim = GetComponent<Animator>();
        playerController = GetComponent<ThirdPersonPlayerController>();
        characterController = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        myAudio = GetComponent<AudioSource>();
    }
    protected void StartCooldown()
    {
        StartCoroutine("Cooldown");
    }

    protected void StopCooldown()
    {
        StopCoroutine("Cooldown");
    }
    IEnumerator Cooldown()
    {
        WaitForSeconds wait = new WaitForSeconds(cooldown);
        yield return wait;
        canAbility = true;
    }
}
