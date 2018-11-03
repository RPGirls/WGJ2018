using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject firstRoom;
    public GameObject secondRoom;
    public GameObject finalCredits;
    public GameObject creditsScreen;
    public GameObject pauseScreen;
    public GameObject controlsScreen;

    private bool paused = false;

    private int count = 0;

    public int numberToChangeRoom = 10;

    private void Awake()
    {
        menuScreen.SetActive(true);
        firstRoom.SetActive(false);
        secondRoom.SetActive(false);
        finalCredits.SetActive(false);
        creditsScreen.SetActive(false);
        pauseScreen.SetActive(false);
        controlsScreen.SetActive(false);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        menuScreen.SetActive(false);
        firstRoom.SetActive(true);
    }

    public void CreditsScreen()
    {
        menuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ControlsScreen()
    {
        menuScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void BackToMenu(GameObject screen)
    {
        screen.SetActive(false);
        menuScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseScreen()
    {
        if (paused)
        {
            Debug.Log("volta p jogo");
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
        else
        {
            Debug.Log("pause");
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        paused = !paused;
    }

    public void BackFromPause()
    {
        Debug.Log("volta p jogo");
        paused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuScreen.activeInHierarchy && !creditsScreen.activeInHierarchy && !finalCredits.activeInHierarchy)
            {
                PauseScreen();
            }
        }
    }

    public void Counter()
    {
        count++;
        Debug.Log("count: " + count);
        if (count == 6)
        {
            FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>().ControlEnemies(true);
        }

        if(count == numberToChangeRoom)
        {
            FindObjectOfType<DialogController>().GetComponent<DialogController>().ChangeDialog(2);
        }
    }
}
