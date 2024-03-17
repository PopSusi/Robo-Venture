using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float HP;
    [SerializeField]
    protected float damageDelay;
    [SerializeField]
    protected bool vulnerable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            HP -= damage;
            vulnerable = false;
            Debug.Log("Owie");
            Debug.Log("takingdamage");
            StartCoroutine("DamageDelay");
            
        }
    }

    IEnumerator DamageDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(damageDelay);
        yield return wait;
        vulnerable = true;
    }
}
