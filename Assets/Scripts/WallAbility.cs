using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallAbility : Ability
{
    [SerializeField] GameObject hitbox;
    [SerializeField] float lockoutTime;
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
        if(context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            if (canAbility && !punching)
            {
                StartChargePunch();
            }
        }
    }
    //Begin charge punch by setting a bool to stop player from repunching, and stop further WASD movement
    IEnumerator StartChargePunch()
    {
        punching = true;
        //RETURN MOVEMENT CONTROLS
        //BACK TO ANIMATION STATE
        WaitForSeconds wait = new WaitForSeconds(lockoutTime);
        yield return wait;
        ChargePunch();
    }

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
    private void ChargePunch()
    {
        myHitbox = Instantiate(hitbox, transform);
        punching = false;
        StartCooldown();
        CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
    }
}
