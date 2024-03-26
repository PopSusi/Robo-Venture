using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchAbility : Ability
{
    public int punchIndex;
    public GameObject[] hitboxes;
    public Vector3[] offset;
    private void Awake()
    {
        input.actions["Punch"].performed+=OnPunch;
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if(canAbility){
            canAbility = false;
            punchIndex += 1; //Cycle Through Punch 1/2
            punchIndex = punchIndex == 4 ? 1 : punchIndex; //Cycle Through Punch 2/2 
            Instantiate(hitboxes[punchIndex - 1], transform.position + offset[punchIndex -1], Quaternion.identity);
            anim.Play("Punch");
            StartCoroutine("PunchDelay");
        }

    }
    IEnumerator PunchDelay(){
        yield return new WaitForSeconds(.15f);
        canAbility = true;
    }
}
