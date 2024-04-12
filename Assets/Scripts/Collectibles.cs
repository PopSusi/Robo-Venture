using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] protected AudioClip shimmerSFX;
    [SerializeField] protected AudioSource myAudio;
    private void Start()
    {
        if (CollectedCheck(this.gameObject))
        {
            Destroy(gameObject);
        }
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = shimmerSFX;
        myAudio.loop = true;
        myAudio.Play();
    }
    public bool CollectedCheck(GameObject go)
    {
        return Settings.Collected.Contains(go);
    }
}
