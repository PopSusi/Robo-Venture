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
    public GameObject playerCurr;
    public GameObject GOobjUI; //ObjectiveUI
    public int objIndex = 0; //ObjectiveIndex for String[]
    public string[] objText; //List of Objective for a level
	public Transform[] checkPoints;
	public int chkpntIndexLevels;
	protected virtual void RespawnPlayer(){
		playerCurr = Instantiate(playerPrefab,
			checkPoints[chkpntIndexLevels].position,
			checkPoints[chkpntIndexLevels].rotation);
	}
    protected void FindPlayer()
    {
        playerCurr = GameObject.Find("player");
    }
}