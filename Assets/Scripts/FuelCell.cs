using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : Collectibles
{
    AudioClip winSFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThirdPersonPlayerController>().fuelCellsTotal++;
            myAudio.loop = false;
            myAudio.clip = winSFX;
            myAudio.Play();
            Destroy(this.gameObject);
        }
    }
}
