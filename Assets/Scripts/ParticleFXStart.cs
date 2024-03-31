using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXStart : MonoBehaviour
{
    public ParticleSystem FX;

    private void OnTriggerEnter(Collider other)
    {
        FX.Play();
    }
}
