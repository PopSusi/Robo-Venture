using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleAbility : Ability
{
    Camera cam;
    [SerializeField]
    float grappleDistance;
    Transform grappleTarget;
    bool isGrappling;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
        player = this.gameObject;
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<ThirdPersonPlayerController>();
        input.actions["Grapple"].performed += OnGrapple;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrappling)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grappleDistance))
        {
            if (hit.collider.CompareTag("GrapplePoint"))
            {
                grappleTarget = hit.collider.transform;
            }
            else
            {
                grappleTarget = null;
            }
        }
        else
        {
            grappleTarget = null;
        }
        //print(grappleTarget);
    }
    private void FixedUpdate()
    {
        if (!isGrappling)
        {
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, grappleTarget.position, 5 * Time.fixedDeltaTime);
        if (Vector3.Distance(grappleTarget.position, this.transform.position) <= 0.1)
        {
            GetComponent<Animator>().SetBool("Grapple", false);
            isGrappling = false;
        }
    }

    void OnGrapple(InputAction.CallbackContext context)
    {
        if (grappleTarget == null) { return; }
        if (unlocked)
        {
            isGrappling = true;
            anim.SetBool("Grapple", true);
            StartCooldown();
            CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
        }
    }
}
