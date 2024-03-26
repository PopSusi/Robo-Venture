using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testboxes : MonoBehaviour
{
     float timeAlive;

     float duration = .25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > duration)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
