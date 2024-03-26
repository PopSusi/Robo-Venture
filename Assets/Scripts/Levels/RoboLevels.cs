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
	public GameObject[] checkPoints;
	public int chkpntIndexLevels;
	public bool overrideSpawn;
	public int overrideSpawnIndex;
	public AudioClip BGM;
	public virtual void RespawnPlayer(){
		if (!overrideSpawn)
		{
			playerCurr = Instantiate(playerPrefab,
				checkPoints[chkpntIndexLevels].transform.position,
				checkPoints[chkpntIndexLevels].transform.rotation);
		}
		else
		{
			playerCurr = Instantiate(playerPrefab,
				checkPoints[overrideSpawnIndex].transform.position,
				checkPoints[overrideSpawnIndex].transform.rotation);
		}
	}
    protected void FindPlayer()
    {
        playerCurr = GameObject.Find("player");
    }
}