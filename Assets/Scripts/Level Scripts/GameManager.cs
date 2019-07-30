using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [Header("Attributes")]

    private bool pause = false;

    private bool inGame = false;

    private int levelIndex = 0;

    [Space]

    [Header("References")]

    public GameObject inGameUI;

    public GameObject pauseUI;

    public GameObject settingsUI;

    public TextManager textManager;

    public KeyManager keyManager;

    private void Start()
    {
        Time.timeScale = 1;
    }
    
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 1)
            inGame = true;

        if (inGame)
            HandleGame();

    }

    private void HandleGame()
    {
        textManager.inGame = true;
        keyManager.inGame = true;
        Debug.Log("Hy");

        if (InputManager.instance.GetKeyDown("pause"))
        {
            if (!pause)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void LoadNextLevel()
    {
        if (levelIndex < SceneManager.sceneCount - 2)
        {
            levelIndex++;
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            MainMenu();
        }
    }

    public void StartGame()
    {
        levelIndex = 1;
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadCurrentLevel()
    {
        textManager.inGame = true;
        keyManager.inGame = true;
        inGame = true;
        SceneManager.LoadScene(levelIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
        pause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
        pause = false;
    }

    public void SettingsOn()
    {
        settingsUI.SetActive(true);
        pauseUI.SetActive(false);
    }
    
    public void SettingsOff()
    {
        settingsUI.SetActive(false);
        pauseUI.SetActive(true);
    }
    
    public void MainMenu()
    {
        textManager.inGame = false;
        keyManager.inGame = false;
        inGame = false;
        Application.LoadLevel(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
