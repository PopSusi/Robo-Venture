using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunDetect : MonoBehaviour
{
    public GameObject AOE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //IMPLEMENT TARGET = FIND GROUND POINT
        //FOR NOW TARGET == WHERE IT LANDS
        Instantiate(AOE, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
