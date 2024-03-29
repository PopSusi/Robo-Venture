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
    public enum ability {Dash, Grapple, Charge, Nulled};
    Image currMask;
    private int index;
    private float time;
    Dictionary<ability, int> abilityIndex = new Dictionary<ability, int>();
    private int slotsActive = 0;
    public void Start()
    {
        CDMInstance = this;
    }
    public void CooldownMaskStart(Sprite theSprite)
    {
        Debug.Log(slots.Length);
        slots[slotsActive].sprite = theSprite;
        slots[slotsActive].transform.parent.gameObject.SetActive(true);
        currMask = masks[slotsActive];
        StartCoroutine("CooldownMask");
        //slotsActive++;
    }

    public IEnumerator CooldownMask()
    {
        if (currMask != null)
        {
            bool operating = true;
            float timeActive = 0f;
            float originalSize = currMask.rectTransform.rect.width;
            while (operating)
            {
                timeActive += Time.deltaTime;
                currMask.gameObject.SetActive(true);
                currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (1 - timeActive / time) * originalSize);
                yield return new WaitForEndOfFrame();
                if (timeActive >= time)
                {
                    currMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize);
                    currMask.gameObject.SetActive(false);
                    operating = false;
                    slotsActive--;
                }
            }
            yield return null;
        }
    }
}
