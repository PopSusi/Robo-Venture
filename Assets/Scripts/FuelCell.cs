using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : Collectibles
{
    AudioClip winSFX;
    [SerializeField] private bool debugNoMenu;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           ThirdPersonPlayerController.fuelCellsTotal++;
            myAudio.loop = false;
            myAudio.clip = winSFX;
            myAudio.Play();
            Destroy(this.gameObject);
            if (!debugNoMenu)
            {
                UIManager.instance.WinLevel();
            }
        }
    }
}
