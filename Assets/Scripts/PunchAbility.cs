using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchAbility : Ability
{
    [field: Header("Ability Sub-Class")]
    [SerializeField] private int punchIndex = 0;
    [SerializeField] private GameObject[] hitboxes;
    [SerializeField] private Vector3[] offset;
    [SerializeField] private float[] timing;
    [SerializeField]float baseDamage;
    [SerializeField] LayerMask enemyLayer;
    
    private void Awake()
    {
        
        Initialize();
        input.actions["Punch"].performed+=OnPunch;
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            StopCoroutine("PunchResetDelay");
            //canAbility = false;
            //GameObject tempbox = Instantiate(hitboxes[punchIndex], transform.position + offset[punchIndex], Quaternion.identity);
            //tempbox.GetComponent<Testboxes>().duration = timing[punchIndex];

            this.transform.rotation = Quaternion.LookRotation(new Vector3(playerController.moveDir.x, 0, playerController.moveDir.y), Vector3.up);
          
            anim.SetTrigger("Punch");
            //StartCoroutine("PunchDelay");
            StartCoroutine("PunchResetDelay");
            gameObject.GetComponent<Animator>().SetInteger("PunchIndex", punchIndex);
            punchIndex += 1; //Cycle Through Punch 1/2
            punchIndex %= 3; //Cycle Through Punch 2/2 
            if (!UIManager.instance.paused)
            {
                gameObject.GetComponent<ThirdPersonPlayerController>().PlaySound(abilitySFX);
            }
        }
    }
    void PunchHit(float damageMulti)
    {
        print("is punching");
        RaycastHit[] hits= Physics.BoxCastAll(this.transform.position+Vector3.up+this.transform.forward,Vector3.one,this.transform.forward,this.transform.rotation,1, enemyLayer);
       foreach(RaycastHit hit in hits)
       {
            hit.collider.GetComponent<Damageable>()?.TakeDamage(damageMulti* baseDamage);
            print("hit "+ hit);
       } 
    }
    IEnumerator PunchDelay(){
        yield return new WaitForSeconds(timing[punchIndex]);
        canAbility = true;
    }
    IEnumerator PunchResetDelay()
    {
        yield return new WaitForSeconds(1f);
        punchIndex = 0;
    }
}
