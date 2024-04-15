using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAbility : Ability
{
    [SerializeField] GameObject hitbox;
    [SerializeField] float lockoutTime;
    [SerializeField] LayerMask DestroyWallLayer;
    public bool punching;
    GameObject myHitbox;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        input.actions["Punch"].performed += OnPunch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnPunch(InputAction.CallbackContext context)
    { 
        if(context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && unlocked)
        {
            Debug.Log("Held");
            if (canAbility && !punching)
            {
                anim.SetTrigger("Charge");
                //StartCoroutine("StartChargePunch");
                //Debug.Log("Activated");
            }
        }
    }
    //Begin charge punch by setting a bool to stop player from repunching, and stop further WASD movement
    /*IEnumerator StartChargePunch()
    {
        punching = true;
        //REMOVE MOVEMENT CONTROLS
        //BACK TO ANIMATION STATE
        WaitForSeconds wait = new WaitForSeconds(lockoutTime);
        yield return wait;
        ChargePunch();
        Debug.Log("BIG PUNCH");
    }*/

    //Public method for other moves to end ability (like jumping or dashing)
    public void StopChargePunch(string AnimationState)
    {
        if (punching)
        {
            StopAllCoroutines();
            punching= false;
            //RETURN MOVEMENT CONTROLS
            //BACK TO ANIMATION STATE
        } else
        {
            Destroy(myHitbox);
            punching = false;
        }
    }
    //Spawn the box
    private void ChargePunchEvent()
    {
        RaycastHit[] hits = Physics.BoxCastAll(this.transform.position + Vector3.up + this.transform.forward, Vector3.one, this.transform.forward, this.transform.rotation, 1, DestroyWallLayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("DestroyableWall"))
            {
                hit.collider.GetComponent<DestroyableWall>()?.Destroy();
            }
            Debug.Log($"Hit: {hit.collider.gameObject.name}");
            //hit.collider.GetComponent<Damageable>()?.TakeDamage(damageMulti * baseDamage);
            //print("hit " + hit);
        }
        punching = false;
        StartCooldown();
        CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
        Debug.Log("Should Spawn");
    }
}
