using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : RoboLevels
{
    public Transform[] spawnPoints; //In order: HubInitial, HubShip, Dash, Grapple, Wall
    // Start is called before the first frame update
    void Start()
    {
        int temp = (int) currLevel;
        if (overrideSpawn)
        {
	        Instantiate(playerPrefab, spawnPoints[temp].position, spawnPoints[temp].rotation);
        }
        FindPlayer();
		Debug.Log("Fuck you");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
