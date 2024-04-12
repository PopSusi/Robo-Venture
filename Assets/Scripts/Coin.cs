using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectibles
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.instance.CoinAlert();
            other.gameObject.GetComponent<ThirdPersonPlayerController>().CoinsCollected++;
            Settings.Collected.Add(this.gameObject);
            Destroy(gameObject);
        }
    }
}
