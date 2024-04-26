using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class UIManager : LevelData
{
    [Header("Menu Components")][Tooltip("List of Menus that will be referenced.")] public GameObject pauseMenu;
    public GameObject optionsMenu, verifyMenu, howToMenu, KeyboardMenu, ControllerMenu, DeathMenu, returnToHubUI, FuelCellMenu;
    public TextMeshProUGUI warningUI, ObjectiveText, TutorialText, CoinTracker, FuelTracker;
    public Image infiniteHealthIcon, allModChipIcon, hardModeIcon;
    [Tooltip("Mask to modify HPBar")] public Image HPBarMask;
    List<GameObject> MenuList = new List<GameObject>();
    //GameplayComponents
    private GameObject playerCurr;
    private PlayerInput input;
    private ThirdPersonPlayerController playerRef;
    public bool paused;
    public static UIManager instance;
    [SerializeField] Toggle amToggle;
    [SerializeField] Toggle infHToggle;
    [SerializeField] Toggle hmToggle;

    private float HPBarMaskSize;

    //Options
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (HPBarMask != null)
        {
            HPBarMaskSize = HPBarMask.rectTransform.rect.width;
        }
        amToggle.isOn = Settings.AllModChips;
        infHToggle.isOn = Settings.infiniteHealth;
        hmToggle.isOn = Settings.hardMode;
        Debug.Log($"allModChips: {Settings.allModChips}, Infinite Health: {Settings.infiniteHealth}, Hard: {Settings.hardMode}");
        StartCoroutine("PlayerSetup");
    }
    IEnumerator PlayerSetup()
    {
        yield return new WaitForSeconds( .1f );
        playerCurr = ThirdPersonPlayerController.instance?.gameObject;
        if (playerCurr != null)
        {
            playerRef = playerCurr.GetComponent<ThirdPersonPlayerController>();
            input = playerCurr.GetComponent<PlayerInput>();
            print(input);
            //Need to switch and switch back to set Pause for UI and Player maps

            input.actions["Pause"].performed += OnPause;

            //input.SwitchCurrentActionMap("UI");
            //input.actions["Pause"].performed+=OnPause;
            //input.SwitchCurrentActionMap("Player");
            if (MenuList.Count == 0)
            {
                FillList();
            }
        }
        SetAllModChips(Settings.AllModChips);
        SetInfiniteHealth(Settings.InfiniteHealth);
        SetHardMode(Settings.HardMode);
    }


    public void EndGame(){
		Application.Quit();
	}
    private void OnPause(InputAction.CallbackContext context)
    {

        print("should pause");
        PauseGame();
    }

    public void LoadGame(string level) //yay
    {
    	if(level == "Start"){
	    Cursor.visible = true;
     	}
        string tempString = level + "Level";
        Time.timeScale = 1f;
        if (RoboLevels.instance!= null)
        {
            Settings.prevLevel = RoboLevels.instance.currLevel;
        }
        SceneManager.LoadScene(tempString, LoadSceneMode.Single);
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        if (!paused)
        { //not currently paused
            paused = true;
            Time.timeScale = 0;
            //input.SwitchCurrentActionMap("UI"); //Go to UI Controls for controller
            MenuList[0].SetActive(true);
            Cursor.visible = true;
        }
        else
        { //currently paused
            paused = false;
            Time.timeScale = 1;
            //input.SwitchCurrentActionMap("Player"); //Go to Gameplay Controls
            foreach (GameObject menuObj in MenuList)
            { //Cycle through all GameObjects and disable them
                if (menuObj != null)
                {
                    menuObj.SetActive(false);
                }
            }
            Cursor.visible = false;
        }
    }
    private void FillList()
    {
        MenuList.Add(pauseMenu);
        MenuList.Add(optionsMenu);
        MenuList.Add(verifyMenu);
        MenuList.Add(howToMenu);
        MenuList.Add(KeyboardMenu);
        MenuList.Add(ControllerMenu);
        MenuList.Add(DeathMenu);
        MenuList.Add(returnToHubUI);
        MenuList.Add(FuelCellMenu);
    }
    public void HealthbarUpdate(float HPCurr)
    {
        HPBarMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (HPCurr / 6) * HPBarMaskSize);
    }
    public void UpdateKillTimer(int time)
    {
        warningUI.gameObject.SetActive(true);
        warningUI.text = time.ToString();
    }
    public void Death()
    {
        Time.timeScale = 0f;
        warningUI.gameObject.SetActive(true);
        warningUI.text = "Try Again?";
        DeathMenu.gameObject.SetActive(true);
        Cursor.visible = true;
    }
    public void Win()
    {
        Time.timeScale = 0f;
        paused = true;
        warningUI.gameObject.SetActive(true);
        warningUI.text = "You've gotten all the Fuel Cells! Wanna play again?";
        DeathMenu.gameObject.SetActive(true);
        Cursor.visible = true;
    }
    public void WinLevel()
    {
        Time.timeScale = 0f;
        paused = true;
        FuelCellMenu.gameObject.SetActive(true);
        Cursor.visible = true;
    }
    public void Retry()
    {
        //Destroy(ThirdPersonPlayerController.instance.gameObject);
        RoboLevels.instance.RespawnPlayer();
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void CoinAlert()
    {
        warningUI.gameObject.SetActive(true);
        warningUI.text = "BIG COIN!!";
        StartCoroutine("WarningDelay");
    }
    private IEnumerator WarningDelay()
    {
        yield return new WaitForSeconds(3f);
        warningUI.gameObject.SetActive(false);
    }
    public void SetAllModChips(bool value)
    {
        Settings.AllModChips = value;
        if (allModChipIcon != null) { allModChipIcon.gameObject.SetActive(value); }
        try
        {
            playerRef.OptionsInitialize();
        }
        catch (ArgumentException e)
        {
            Debug.Log(e);
        }
    }
    public void SetInfiniteHealth(bool value)
    {
        Settings.InfiniteHealth = value;
        infiniteHealthIcon.gameObject.SetActive(value);
        playerRef.OptionsInitialize();
    }
    public void SetHardMode(bool value)
    {
        Settings.HardMode = value;
        hardModeIcon.gameObject.SetActive(value);
        playerRef.OptionsInitialize();
    }
    public void UpdateCoins(int coins)
    {
        CoinTracker.text = $"Big Coins: {coins} / 3";
    }
    public void UpdateFuel(int fuel)
    {
        FuelTracker.text = $"Fuel Cells: {fuel} / 4";
    }

}
