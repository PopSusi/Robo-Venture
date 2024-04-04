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

    private void Awake()
    {
        Initialize();
        input.actions["Punch"].performed+=OnPunch;
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if(canAbility)
        {
            StopCoroutine("PunchResetDelay");
            canAbility = false;
            GameObject tempbox = Instantiate(hitboxes[punchIndex], transform.position + offset[punchIndex], Quaternion.identity);
            tempbox.GetComponent<Testboxes>().duration = timing[punchIndex];
            anim.SetTrigger("Punch");
            StartCoroutine("PunchDelay");
            StartCoroutine("PunchResetDelay");
            punchIndex += 1; //Cycle Through Punch 1/2
            punchIndex %= 3; //Cycle Through Punch 2/2 
            gameObject.GetComponent<Animator>().SetInteger("PunchIndex", punchIndex);
            gameObject.GetComponent<ThirdPersonPlayerController>().PlaySound(abilitySFX);
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
