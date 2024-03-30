using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboLevels : LevelData
{
    
	//LEVEL
    public static Levels currLevel = Levels.Hub;
	public static RoboLevels instance;
    public GameObject playerPrefab;
    public GameObject playerCurr;
	

	//UI
    public GameObject GOobjUI;
    public string[] objectiveText; //List of Objective for a level
    public int objectiveIndex = 0; //ObjectiveIndex for String[]

	//CHECKPOINTS
	public GameObject[] checkPoints; //List of Checkpoints
	public int chkpntIndexLevels; //Current Checkpoint
	public bool overrideSpawn; //Set true if you want to go to certain checkpoint
	public int overrideSpawnIndex; //Override Checkpoint

	//AUDIO
	public AudioClip BGM;
    protected void Start()
    {
		instance = this;
    }
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
        playerCurr = ThirdPersonPlayerController.instance.gameObject;
    }

}