using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplePoint : MonoBehaviour
{
    public Image anchors;
    public bool active;
    public void UpdateAnchors(float size)
    {
        if (active)
        {
            anchors.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            anchors.transform.GetChild(0).gameObject.SetActive(size == 205);
        }
    }
    public void Deactivate()
    {
        active = false;
        anchors.gameObject.SetActive(false);
        anchors.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Activate()
    {
        anchors.gameObject.SetActive(true);
        active = true;
    }
}
