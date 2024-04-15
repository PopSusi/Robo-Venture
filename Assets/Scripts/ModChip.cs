using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModChip : MonoBehaviour
{
    public enum Type { Dash, Grapple, Wall};
    [Tooltip("Specifying which ability to unlock.")] public Type type;
    [Tooltip("Tutorialbox to set active when grabbing ModChip.")] public GameObject TutorialBox;
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
            Destroy(this.transform.parent.gameObject);
            TutorialBox.SetActive(true);
        }
    }
}
