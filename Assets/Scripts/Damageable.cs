using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Damageable 
{
    float HP{ get; set; }  
    float damageDelay{ get; set; }
    bool vulnerable{ get; set; }

    public abstract void TakeDamage(float damage);

    protected IEnumerator DamageDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(damageDelay);
        yield return wait;
        vulnerable = true;
    }
}
