using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : RoboLevels
{
    public Transform[] spawnPoints; //In order: HubInitial, HubShip, Dash, Grapple, Wall
    // Start is called before the first frame update
    void Start()
    {
        chkpntIndexLevels = (int) Settings.prevLevel;
        if (overrideSpawn)
        {
            chkpntIndexLevels = overrideSpawnIndex;
        }
        base.Start();
        Debug.Log("Fuck you");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
