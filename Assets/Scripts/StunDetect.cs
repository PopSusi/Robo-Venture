using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunDetect : MonoBehaviour
{
    public GameObject AOE;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit: " + other.gameObject.tag);
        //IMPLEMENT TARGET = FIND GROUND POINT
        //FOR NOW TARGET == WHERE IT LANDS
        Instantiate(AOE, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
