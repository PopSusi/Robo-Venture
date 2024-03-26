using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    //Menu Components
    public GameObject pauseMenu, optionsMenu, verifyMenu, howToMenu, KeyboardMenu, ControllerMenu;
    List<GameObject> MenuList = new List<GameObject>();

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
                playerRef.OptionsInitialize();
            }}
    }
    public bool infiniteHealth;
    public bool hardMode;
    // Start is called before the first frame update
    void Start()
    {
        playerCurr = gameObject.GetComponent<RoboLevels>().playerCurr;
        playerRef = playerCurr.GetComponent<ThirdPersonPlayerController>();
        pI = playerCurr.GetComponent<PlayerInput>();
        FillList();
    }

    public void EndGame(){
		Application.Quit();
	}
	public void PauseGame(){
		if(!paused){ //not currently paused
			paused = true;
			Time.timeScale = 0;
			pI.SwitchCurrentActionMap("UI");
			MenuList[0].SetActive(true);
		} else { //currently paused
			paused = false;
			Time.timeScale = 1;
			pI.SwitchCurrentActionMap("Player");
			foreach(GameObject menuObj in MenuList){
                menuObj.SetActive(false);
            }
		}
	} 
    private void FillList(){
        MenuList.Add(pauseMenu);
        MenuList.Add(optionsMenu);
        MenuList.Add(verifyMenu);
        MenuList.Add(howToMenu);
        MenuList.Add(KeyboardMenu);
        MenuList.Add(ControllerMenu);
    }
}
