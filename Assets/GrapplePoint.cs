using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplePoint : MonoBehaviour
{
    public Image anchors;
    public bool active;
    /// <summary>
    /// Change size of anchor container, making it smaller/larger based on distance. 
    /// If closest possible, activate little dot.
    /// </summary>
    /// <param name="size">Expands size of anchor container to this value (Between 205 - 275).</param>
    public void UpdateAnchors(float size)
    {
        if (active)
        {
            anchors.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            anchors.transform.GetChild(0).gameObject.SetActive(size == 205); //Activates little point
        }
    }
    /// <summary>
    /// Turn off Grapple Point UI.
    /// </summary>
    public void Deactivate()
    {
        active = false;
        anchors.gameObject.SetActive(false);
        anchors.transform.GetChild(0).gameObject.SetActive(false);
    }
    /// <summary>
    /// Turn on Grapplepoint UI.
    /// </summary>
    public void Activate()
    {
        anchors.gameObject.SetActive(true);
        active = true;
    }
}
