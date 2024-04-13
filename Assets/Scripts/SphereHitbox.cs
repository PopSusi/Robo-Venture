using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHitbox : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("DestroySelf");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyableWall"))
        {
            foreach(Transform aChild in other.transform)
            {
                Rigidbody otherRb = aChild.gameObject.
                    GetComponent<Rigidbody>();
                otherRb.isKinematic = false;
                otherRb.AddExplosionForce(10, this.transform.position, this.transform.localScale.x);
            }
        } 
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1f);
        }
    }
    private IEnumerator DestroySelf()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        Destroy(gameObject);
    }
}
