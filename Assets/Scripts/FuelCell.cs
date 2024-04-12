using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    public AudioClip shimmerSFX, winSFX;
    private AudioSource myAudio;
    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = shimmerSFX;
        myAudio.loop = true;
        myAudio.Play();

    }
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
