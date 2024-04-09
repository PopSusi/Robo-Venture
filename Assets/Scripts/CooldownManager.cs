using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager CDMInstance;
    [Tooltip("Each slot from HUD.")]
    public Image[] slots;
    [Tooltip("Each slot mask from HUD.")]
    public Image[] masks;
    Image currMask;
    private int slotsActive = 0;

    //Establish Singleton
    public void Start()
    {
        if (CDMInstance == null)
        {
            CDMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Begin Cooldown UI Animation.
    /// </summary>
    /// <param name="theSprite">Sprite related to ability.</param>
    /// <param name="secs">Duration of Cooldown per ability</param>
    public void CooldownMaskStart(Sprite theSprite, float secs)
    {
        slots[slotsActive].sprite = theSprite;
        slots[slotsActive].transform.parent.gameObject.SetActive(true);
        currMask = masks[slotsActive];
        StartCoroutine(CooldownMask(secs));
        slotsActive++;
    }

    //Updates Mask, draining from right to left, until it's at 0.
    IEnumerator CooldownMask(float time)
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
