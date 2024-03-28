using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunDamage : MonoBehaviour
{
    public float damage;
    public float duration;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= duration)
        {
            Byebye();
        }
    }

    void Byebye()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
