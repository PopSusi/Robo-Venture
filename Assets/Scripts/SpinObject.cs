using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour

{
    public Vector3 spinSpeed;

    void Update()
    {
        // Update rotation using spinSpeed
        transform.localRotation *= Quaternion.Euler(spinSpeed * Time.deltaTime);
    }
}

