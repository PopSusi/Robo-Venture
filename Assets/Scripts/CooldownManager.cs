using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager CDMInstance;
    public Image[] masks;
    public Image[] slots;
    Image currMask;
    private int slotsActive = 0;
    public void Start()
    {
        CDMInstance = this;
    }
    public void CooldownMaskStart(Sprite theSprite, float secs)
    {
        slots[slotsActive].sprite = theSprite;
        slots[slotsActive].transform.parent.gameObject.SetActive(true);
        currMask = masks[slotsActive];
        StartCoroutine(CooldownMask(secs));
        slotsActive++;
    }

    public IEnumerator CooldownMask(float time)
    {
        if (currMask != null)
        {
            bool operating = true;
            float timeActive = 0f;
            float timeTotal = time;
            float originalSize = currMask.rectTransform.rect.width;
            while (operating)
            {
                //Debug.Log("Tickin down");
                timeActive += Time.deltaTime;
                currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (1 - timeActive / timeTotal) * originalSize);
                yield return new WaitForEndOfFrame();
                if (timeActive >= timeTotal)
                {
                    Debug.Log("It's done");
                    currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize);
                    currMask.transform.parent.gameObject.SetActive(false);
                    operating = false;
                    slotsActive--;
                }
            }
            yield return null;
        }
    }
}
