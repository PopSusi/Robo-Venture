using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWall : MonoBehaviour
{
    UIManager UIMan;
    GameObject playerObj;
    bool killTimer;
    float killTimeCurr = 5f;

    // Start is called before the first frame update
    void Start()
    {
        UIMan = UIManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (killTimer)
        {
            if (killTimeCurr > 0f)
            {
                killTimeCurr -= Time.deltaTime;
                UIMan.UpdateKillTimer((int) killTimeCurr);
            }
            else
            {
                KillPlayer();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObj = other.gameObject;
            killTimer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObj = other.gameObject;
            killTimer = false;
            killTimeCurr = 5;
            UIMan.warningUI.gameObject.SetActive(false);
        }
    }
    void KillPlayer()
    {
        killTimer = false;
        killTimeCurr = 5;
        playerObj.GetComponent<ThirdPersonPlayerController>().Die();
    }
}
