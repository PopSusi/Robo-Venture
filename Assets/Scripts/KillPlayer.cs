using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public RoboLevels GM;
    private void Start(){
        GM = GameObject.FindWithTag("LevelGM").GetComponent<RoboLevels>();
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
