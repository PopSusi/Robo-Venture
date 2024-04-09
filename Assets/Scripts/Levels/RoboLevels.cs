using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboLevels : LevelData
{
    
	[field: Header("Level Setup")]//LEVEL
    public static Levels currLevel = Levels.Hub;
	public static RoboLevels instance;
    [Tooltip("Player prefab to spawn.")]
    public GameObject playerPrefab;
	[Tooltip("Set at runtime/respawn, holds player object.")]
    public GameObject playerCurr;
	

    [field: Header("Objective UI")]//UI
    [Tooltip("UI Element to add tutorial text to.")]
    public GameObject GOobjUI;

	[field: Header("Checkpoints")]//CHECKPOINTS
    [Tooltip("Set at Runtime - List of checkpoints.")]
    public GameObject[] checkPoints; //List of Checkpoints
    [Tooltip("Set when interacting with checkpoint - Chooses which checkpoint to spawn at.")]
    public int chkpntIndexLevels; //Current Checkpoint
    [Tooltip("Debug bool to start at certain checkpoints.")]
    public bool overrideSpawn; //Set true if you want to go to certain checkpoint
    [Tooltip("Which checkpoint you want to spawn at - Based on index of said checkpoint.")]
    public int overrideSpawnIndex; //Override Checkpoint

	[field: Header("Audio")]//AUDIO
    [Tooltip("Level's background music.")]
    public AudioClip BGM;

    //Establish singleton
    protected virtual void Awake()
    {
		if (instance == null)
		{
            instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
    }
    /// <summary>
    /// Respawn Player at previously set checkpoint
    /// </summary>
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