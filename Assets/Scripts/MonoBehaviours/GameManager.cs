using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]

    public bool inGame = false;

    private bool pause = false;

    private int levelIndex;

    private int levelCount;

    [Space]

    [Header("References")]

    public GameObject mainMenuUI;

    public GameObject pauseUI;

    public GameObject settingsUI;

    public GameObject statsUI;

    public Button continueButton;

    public TextManager textManager;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        levelCount = SceneManager.sceneCountInBuildSettings;
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.buildIndex == 0)
        {
            mainMenuUI.SetActive(true);
            inGame = false;
            textManager.inGame = false;
            levelIndex = 0;
            continueButton.interactable = false;
        }
        else
        {
            statsUI.SetActive(true);
            inGame = true;
            textManager.inGame = true;
            levelIndex = scene.buildIndex;
        }
    }

    private void Update()
    {
        if (inGame)
        {
            HandlePauseMenu();
        }

        if(levelIndex == 0 && continueButton.interactable == true)
        {
            continueButton.interactable = false;            
        }
        if (levelIndex != 0 && continueButton.interactable == false)
        {
            continueButton.interactable = true;
        }
    }

    private void HandlePauseMenu()
    {
        if (InputManager.instance.GetKeyDown("pause"))
        {
            if(!pause)
                Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        statsUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        statsUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void SettingsOn()
    {
        settingsUI.SetActive(true);
    }

    public void SettingsOff()
    {
        settingsUI.SetActive(false);
    }
    
    public void StartGame()
    {
        levelIndex = 1;
        Time.timeScale = 1;
        mainMenuUI.SetActive(false);
        statsUI.SetActive(true);
        inGame = true;
        textManager.inGame = true;
        SceneManager.LoadScene(levelIndex);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        inGame = true;
        textManager.inGame = true;
        mainMenuUI.SetActive(false);
        statsUI.SetActive(true);
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        levelIndex++;

        if(levelIndex <= levelCount - 1)
        {
            textManager.prevInGame = false;
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            levelIndex = 0;
            MainMenu();
        }
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        inGame = false;
        textManager.inGame = false;
        statsUI.SetActive(false);
        pauseUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}