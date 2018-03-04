using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is attached to Canvas, its public gameobjects reference the pause and end screens (set in inspector)
public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject endScreenUI;
    private AudioSource gameMusicSource;

    private bool fadingIn, fadingOut;

    private void Start()
    {
        gameMusicSource = GetComponent<AudioSource>();
        gameMusicSource.volume = 0;
        fadingIn = true;
        print("sartrerd");
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn) { 
            if (gameMusicSource.volume < 1)
            {
                gameMusicSource.volume += Time.fixedDeltaTime;
            } else
            {
                fadingIn = false;
            }
        }
        if (fadingOut)
        {
            if (gameMusicSource.volume > 0.5)
            {
                gameMusicSource.volume -= Time.fixedDeltaTime;
            } else
            {
                fadingOut = false;
            }
        }

        //Pause menu is displayed when pushing esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }
    }

    public void Resume()
    {
        fadingIn = true;
        fadingOut = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        fadingIn = false;
        fadingOut = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    //Retry used in endscreen
    public void Retry()
    {
        fadingIn = true;
        Resume();
        SceneManager.LoadScene("Tommi");   //change name accordingly
    }

    public void LoadMenu()
    {
        fadingOut = true;
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
