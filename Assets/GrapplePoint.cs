using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplePoint : MonoBehaviour
{
    [SerializeField] private Image anchors;
    public void UpdateAnchors(float size)
    {
        anchors.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
    }
}
