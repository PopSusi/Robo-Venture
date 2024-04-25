using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private RoboLevels GM;
    private void Start(){
        GM = RoboLevels.instance;
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<ThirdPersonPlayerController>().Die();
            GM.RespawnPlayer();
        }
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.GetComponent<Enemy>().Die();
        }
    }
}
