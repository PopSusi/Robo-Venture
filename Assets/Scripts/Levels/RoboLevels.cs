using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboLevels : MonoBehaviour
{
    public enum Levels
    {
        HubInitial = 0,
		HubShip,
        Dash,
        Grapple,
        Wall
    };
    public static Levels currLevel = Levels.HubInitial;
    public GameObject playerPrefab;
    protected GameObject playerCurr;
    public GameObject GOobjUI; //ObjectiveUI
    public string[] objectiveText; //List of Objective for a level
    public int objectiveIndex = 0; //ObjectiveIndex for String[]
	public Transform[] checkPoints;
	public int chkpntIndexLevels;
	public bool overrideSpawn;
	public int overrideSpawnIndex;
	protected virtual void RespawnPlayer(){
		if (!overrideSpawn)
		{
			playerCurr = Instantiate(playerPrefab,
				checkPoints[chkpntIndexLevels].position,
				checkPoints[chkpntIndexLevels].rotation);
		}
		else
		{
			playerCurr = Instantiate(playerPrefab,
				checkPoints[overrideSpawnIndex].position,
				checkPoints[overrideSpawnIndex].rotation);
		}
	}
    protected void FindPlayer()
    {
        playerCurr = GameObject.Find("player");
    }
}