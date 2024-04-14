using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private RoboLevels GM;
    [SerializeField] bool fixedPlace;
    [Tooltip("Starting from 0-n, this tells the Game Manager which checkpoint to spawn from.")]
    public int myIndex;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            player = other.gameObject;
            GM.chkpntIndexLevels = myIndex;
        }
    }
    // Start is called before the first frame update
    private void Start(){
        if (!fixedPlace)
        {
            GM = RoboLevels.instance;
            GM.checkPoints[myIndex] = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
