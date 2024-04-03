using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModChip : MonoBehaviour
{
    public enum Type { Dash, Grapple, Wall};
    public Type type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case Type.Dash:
                    other.gameObject.GetComponent<ThirdPersonPlayerController>().EnableDash();
                    break;
                case Type.Grapple:
                    other.gameObject.GetComponent<ThirdPersonPlayerController>().EnableGrapple();
                    break;
                case Type.Wall:
                    other.gameObject.GetComponent<ThirdPersonPlayerController>().EnableWall();
                    break;
                default:
                    break;
            }
        }
        Destroy(this.gameObject);
    }
}
