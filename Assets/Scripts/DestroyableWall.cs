using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public void Destroy()
    {
        Debug.Log("Destroy");
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<WallPiece>()?.SetFalse();
        }
    }
}
