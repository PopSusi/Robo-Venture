using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    public virtual void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            HP -= damage;
            vulnerable = false;
            GetComponent<AudioSource>().Play();
            StartCoroutine("DamageDelay");
            
        }
    }
}
