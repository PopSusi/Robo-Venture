using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffMesh : MonoBehaviour
{
    void Awake()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }
}
