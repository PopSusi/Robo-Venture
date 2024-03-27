using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private RoboLevels GM;
    public int myIndex;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            player = other.gameObject;
            GM.chkpntIndexLevels = myIndex;
        }
    }
    // Start is called before the first frame update
    private void Awake(){
        GM = GameObject.FindWithTag("LevelGM").GetComponent<RoboLevels>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
