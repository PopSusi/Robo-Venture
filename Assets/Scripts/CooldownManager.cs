using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public Image HPBarMask, GrenadeMask, DashMask, GrappleMask, ChargeMask;
    Image currMask;
    private int index;
    private float time;
    public void CooldownMaskStart(int indexIn, float timeIn)
    {

        index = indexIn;
        time = timeIn;
        switch (index)
        {
            case 0:
                currMask = GrenadeMask;
                break;
            case 1:
                currMask = DashMask;
                break;
            case 2:
                currMask = GrappleMask;
                break;
            case 3:
                currMask = ChargeMask;
                break;
            default: break;
        }
        Debug.Log(index);
        StartCoroutine("CooldownMask");
    }

    public IEnumerator CooldownMask()
    {
        bool operating = true;
        float timeActive = 0f;
        float originalSize = currMask.rectTransform.rect.width;
        while (operating)
        {
            Debug.Log("running " + timeActive / time);
            timeActive += Time.deltaTime;
            currMask.gameObject.SetActive(true);
            currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (1 - timeActive / time) * originalSize);
            yield return new WaitForEndOfFrame();
            if (timeActive >= time)
            {
                Debug.Log("Complete");
                currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize);
                currMask.gameObject.SetActive(false);
                operating = false;
            }
        }
        yield return null;
    }
}
