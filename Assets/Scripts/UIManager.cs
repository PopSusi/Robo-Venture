using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    //Menu Components
    public GameObject pauseMenu, optionsMenu, verifyMenu;

    //GameplayComponents
    private GameObject playerCurr;
    private PlayerInput pI;
    private ThirdPersonPlayerController playerRef;
    bool paused;

    //Options
    public bool allModChips;
    public bool AllModChips{
        get{return allModChips;}
        set{allModChips = value;
            if(playerRef != null){
                playerRef.AbilitiesInitialize();
            }}
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCurr = gameObject;
        playerRef = playerCurr.GetComponent<ThirdPersonPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame(){
		Application.Quit();
	}
	public void PauseGame(){
		if(!paused){ //not currently paused
			paused = true;
			Time.timeScale = 0;
			pI.SwitchCurrentActionMap("UI");
			pauseMenu.SetActive(true);
		} else { //currently paused
			paused = false;
			Time.timeScale = 1;
			pI.SwitchCurrentActionMap("Player");
			pauseMenu.SetActive(false);
			optionsMenu.SetActive(false);
			verifyMenu.SetActive(false);
		}
	} 
}
