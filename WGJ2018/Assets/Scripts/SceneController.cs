using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject firstRoom;
    public GameObject secondRoom;
    public GameObject finalCredits;
    public GameObject creditsScreen;
    public GameObject pauseScreen;
    public GameObject controlsScreen;
    public GameObject intro;
    public GameObject game;

    private bool paused = false;
    private bool introOff = true;

    private int count = 50;
    private int numberOfEnemies = 0;

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
        intro.SetActive(false);
        game.SetActive(false);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        menuScreen.SetActive(false);
        intro.SetActive(true);
        intro.GetComponent<PlayableDirector>().Play();
        introOff = false;
    }

    public void SecondRoom()
    {
        firstRoom.SetActive(false);
        secondRoom.SetActive(true);
        count = 50;
        Camera.main.orthographicSize = 5f;
        Camera.main.gameObject.transform.position = new Vector3(0f, 0f, -10f);
        FindObjectOfType<SpawnerController>().transform.localScale = new Vector3(111.2f, 5.5f, 1f);
        FindObjectOfType<SpawnerController>().transform.localPosition = new Vector3(-383.8f, -227.4f, 1f);
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
        SceneManager.LoadScene("game");
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

        if (intro.GetComponent<PlayableDirector>().state != PlayState.Playing && !introOff)
        {
            introOff = true;
            intro.SetActive(false);
            firstRoom.SetActive(true);
            game.SetActive(true);
            Camera.main.orthographicSize = 3.35f;
            Camera.main.gameObject.transform.position = new Vector3(-1f, -1.65f, -10f);
            FindObjectOfType<SpawnerController>().transform.localScale = new Vector3(75.75f, 3.75f, 1f);
            FindObjectOfType<SpawnerController>().transform.localPosition = new Vector3(-387.52f, -230.32f, 1f);
        }
    }

    public void Counter(bool isGood)
    {
        numberOfEnemies++;
        if (isGood)
        {
            count += 10;
        }
        else
        {
            count -= 10;
            if (count < 0)
            {
                count = 0;
            }
        }

        GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetComponent<Slider>().value = count;

        Debug.Log("count: " + count);
        if (numberOfEnemies == 6)
        {
            FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>().ControlEnemies(true);
        }

        if (count >= 100)
        {
            FindObjectOfType<DialogController>().GetComponent<DialogController>().ChangeDialog(2);
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("badObject");
            GameObject[] objs02;
            objs02 = GameObject.FindGameObjectsWithTag("goodObject");

            foreach (GameObject obj in objs)
            {
                obj.GetComponent<EnemyController>().SetCanMove(false);
            }

            foreach (GameObject obj in objs02)
            {
                obj.GetComponent<EnemyController>().SetCanMove(false);
            }
        }
    }

    public void Win()
    {
        FindObjectOfType<ScrollBackground>().StopScrolling();
    }
}
