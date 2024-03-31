using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            UIManager.instance.Win();
        }
    }
}
