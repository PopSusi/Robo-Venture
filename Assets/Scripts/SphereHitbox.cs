using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyableWall"))
        {
            other.gameObject.GetComponent<DestroyableWall>().Destroy();
        } 
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1f);
        }
    }
}
