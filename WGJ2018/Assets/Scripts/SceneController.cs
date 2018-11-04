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

    public GameObject witch;
    public GameObject princess;
    public GameObject bubble;

    public GameObject cenario01;
    public GameObject cenario02;
    public GameObject[] fogo;

    public AudioSource audioSource;
    public AudioClip treinamento;
    public AudioClip run;

    private bool paused = false;
    private bool introOff = true;
    private bool isSecond = false;

    private int count = 50;
    private int numberOfEnemies = 0;

    public int pointsFirstRoom = 10;
    public int pointsSecondRoom = 5;

   // public Color goodColorBackground;
    public Color goodColorFill;
   // public Color badColorBackground;
    public Color badColorFill;
    public Color initialColorFill;

    private void Awake()
    {
        audioSource.clip = treinamento;
        audioSource.Play();

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
        audioSource.Stop();
        Time.timeScale = 1f;
        menuScreen.SetActive(false);
        intro.SetActive(true);
        intro.GetComponent<PlayableDirector>().Play();
        introOff = false;
    }


    public void SecondRoom()
    {
        isSecond = true;
        firstRoom.SetActive(false);
        secondRoom.SetActive(true);
        count = 50;
        GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetComponent<Slider>().value = count;
        Camera.main.orthographicSize = 5f;
        Camera.main.gameObject.transform.position = new Vector3(0f, 0f, -10f);
        FindObjectOfType<SpawnerController>().transform.localScale = new Vector3(111.2f, 5.5f, 1f);
        FindObjectOfType<SpawnerController>().transform.localPosition = new Vector3(-383.8f, -227.4f, 1f);

        GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = initialColorFill;
    }

    public void CreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void FinalCreditsScreen()
    {
        game.SetActive(false);
        finalCredits.SetActive(true);
    }

    public void ControlsScreen()
    {
        menuScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void BackToMenu(GameObject screen)
    {
        if (screen.name == "Pause")
        {
            SceneManager.LoadScene("game");
        }
        else
        {
            screen.SetActive(false);
            menuScreen.SetActive(true);
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && intro.activeSelf)
        {
            intro.GetComponent<PlayableDirector>().Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuScreen.activeInHierarchy && !creditsScreen.activeInHierarchy && !finalCredits.activeInHierarchy)
            {
                PauseScreen();
            }
        }

        if (intro.GetComponent<PlayableDirector>().state != PlayState.Playing && !introOff)
        {
            audioSource.clip = treinamento;
            audioSource.Play();

            introOff = true;
            intro.SetActive(false);
            firstRoom.SetActive(true);
            game.SetActive(true);
            isSecond = false;
            Camera.main.orthographicSize = 3.35f;
            Camera.main.gameObject.transform.position = new Vector3(-1f, -1.65f, -10f);
            FindObjectOfType<SpawnerController>().transform.localScale = new Vector3(75.75f, 3.75f, 1f);
            FindObjectOfType<SpawnerController>().transform.localPosition = new Vector3(-387.52f, -230.32f, 1f);

            GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = initialColorFill;
        }
    }

    public void Counter(bool isGood)
    {
        numberOfEnemies++;
        int points = 0;
        if (isSecond)
        {
            points = pointsSecondRoom;
        }
        else
        {
            points = pointsFirstRoom;

        }
        if (isGood)
        {
            //GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(0).GetComponent<Image>().color = goodColorBackground;
            GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = goodColorFill;
            count += points;
        }
        else
        {

           // GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(0).GetComponent<Image>().color = badColorBackground;
            GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = badColorFill;

            count -= points;
            if (count < 0)
            {
                count = 0;
            }
        }

        GameObject.FindGameObjectWithTag("progression").transform.GetChild(0).GetComponent<Slider>().value = count;

        if (numberOfEnemies == 6)
        {
            FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>().ControlEnemies(true);
        }

        if (count >= 100)
        {
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("badObject");
            GameObject[] objs02;
            objs02 = GameObject.FindGameObjectsWithTag("goodObject");

            foreach (GameObject obj in objs)
            {
                if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 1)
                {
                    obj.GetComponent<EnemyController>().SetCanMove(false);
                }
                else if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 3)
                {
                    Destroy(obj);
                }
            }

            foreach (GameObject obj in objs02)
            {
                if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 1)
                {
                    obj.GetComponent<EnemyController>().SetCanMove(false);
                }
                else if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 3)
                {
                    Destroy(obj);
                }
            }

            if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 1)
            {
                FindObjectOfType<DialogController>().GetComponent<DialogController>().ChangeDialog(2);
            }
            else if (FindObjectOfType<DialogController>().GetComponent<DialogController>().GetDialog() == 3)
            {
                FindObjectOfType<SpawnerController>().GetComponent<SpawnerController>().ControlEnemies(false);

                GameObject[] cenarios;
                cenarios = GameObject.FindGameObjectsWithTag("cenario");

                foreach (GameObject cenario in cenarios)
                {
                    cenario.GetComponent<ScrollBackground>().StopScrolling();
                }
                princess.GetComponent<PlayerController>().ChangeCanBubble(false);
                bubble.SetActive(false);
                princess.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                princess.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                princess.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                witch.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                witch.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                witch.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                princess.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
                witch.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);

                StartCoroutine("WaitToWin");
            }
        }
    }

    IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(5f);
        princess.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        witch.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        princess.GetComponent<Animator>().SetBool("walking", false);
        princess.GetComponent<Animator>().SetTrigger("stop");
        witch.GetComponent<Animator>().SetTrigger("stop");
        princess.transform.localPosition = new Vector3(-390f, -235.75f, 0f);
        witch.transform.localPosition = new Vector3(-395f, -235.39f, 0f);
        cenario01.SetActive(false);
        cenario02.SetActive(true);
        for (int i = 0; i < fogo.Length; i++)
        {
            fogo[i].SetActive(false);
        }
        FindObjectOfType<DialogController>().GetComponent<DialogController>().ChangeDialog(4);
    }

    public bool isSecondRoom()
    {
        return isSecond;
    }

    public void ChangeToAudioRun()
    {
        audioSource.clip = run;
        audioSource.Play();
    }
}
