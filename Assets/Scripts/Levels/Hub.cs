using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : RoboLevels
{
    public Transform[] spawnPoints; //In order: Hub, Dash, Grapple, Wall
    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
        int temp = (int) currLevel;

        player.transform.position = spawnPoints[temp].position;
        player.transform.rotation = spawnPoints[temp].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
