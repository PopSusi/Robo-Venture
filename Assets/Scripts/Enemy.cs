using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    public CombatTriggers myTrigger;
    [SerializeField] private LayerMask layermask;
    public void Start(){
        Collider[] tempHold = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        myTrigger = tempHold[0].gameObject.GetComponent<CombatTriggers>();
        StartCoroutine("Fuckingdie");
    }
    public override void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            HP -= damage;
            if(!(HP <= 0)){ //Not at zero
                vulnerable = false;
                GetComponent<AudioSource>().Play();
                StartCoroutine("DamageDelay");
            } else {
                Die();
            }
        }
    }
    public void Die(){
        Debug.Log(myTrigger.CheckEnemies(gameObject));
        Destroy(gameObject);
    }
    private IEnumerator Fuckingdie(){
        yield return new WaitForSeconds(3f);
        Die();
    }
    
}
