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
    public GameObject optionsMenu, verifyMenu, howToMenu, KeyboardMenu, ControllerMenu;
    public TextMeshProUGUI warningUI, objUI;
    [Tooltip("Mask to modify HPBar")] public Image HPBarMask;
    List<GameObject> MenuList = new List<GameObject>();
    //GameplayComponents
    private GameObject playerCurr;
    private PlayerInput input;
    private ThirdPersonPlayerController playerRef;
    bool paused;
    public static UIManager instance;

    private float HPBarMaskSize;

    //Options
    public bool allModChips;
    public bool AllModChips
    {
        get { return allModChips; }
        set
        {
            allModChips = value;
            if (playerRef != null)
            {
                playerRef.OptionsInitialize();
            }
        }
    }
    public bool infiniteHealth;
    public bool hardMode;
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

    }
    void Start()
    {
        playerCurr = ThirdPersonPlayerController.instance.gameObject;
        if (playerCurr == null)
        {
            return;
        }
        playerRef = playerCurr.GetComponent<ThirdPersonPlayerController>();
        input = playerCurr.GetComponent<PlayerInput>();
        print(input);
        //Need to switch and switch back to set Pause for UI and Player maps

        input.actions["Pause"].performed+=OnPause;

        //input.SwitchCurrentActionMap("UI");
        //input.actions["Pause"].performed+=OnPause;
        //input.SwitchCurrentActionMap("Player");
        FillList();
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
        string tempString = level + "Level";
        SceneManager.LoadScene(tempString, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        if (!paused)
        { //not currently paused
            paused = true;
            Time.timeScale = 0;
            //input.SwitchCurrentActionMap("UI"); //Go to UI Controls for controller
            MenuList[0].SetActive(true);
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
        verifyMenu.gameObject.SetActive(true);
    }
    public void Win()
    {
        Time.timeScale = 0f;
        warningUI.gameObject.SetActive(true);
        warningUI.text = "You've gotten the Fuel Cell! Wanna play again?";
        verifyMenu.gameObject.SetActive(true);
    }
    public void Retry()
    {
        gameObject.GetComponent<RoboLevels>().RespawnPlayer();
        Time.timeScale = 1f;
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
}