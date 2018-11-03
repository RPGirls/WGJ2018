using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    public GameObject princess;
    public GameObject witch;
    public GameObject dialogueBox;
    public GameObject spawner;

    public Sprite[] firstDialog;
    public Sprite[] secondDialog;
    public Sprite[] thirdDialog;
    public Sprite[] forthDialog;

    private Sprite[] currentDialog;

    private bool dialogIsOn = true;

    private int dialogNumber = 0;

    private void Start()
    {
        currentDialog = firstDialog;
        StartDialog();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogIsOn)
        {
            NextDialog();
        }
    }

    public void ChangeDialog(int number)
    {
        if (number == 2)
        {
            currentDialog = secondDialog;
        }
        if (number == 3)
        {
            currentDialog = thirdDialog;
        }
        if (number == 4)
        {
            currentDialog = forthDialog;
        }
        StartDialog();
    }

    private void StartDialog()
    {
        spawner.GetComponent<SpawnerController>().ControlEnemies(false);
        princess.GetComponent<PlayerController>().ChangeCanBubble(false);
        dialogIsOn = true;
        dialogueBox.SetActive(true);
        NextDialog();
    }

    private void NextDialog()
    {
        if (currentDialog.Length > dialogNumber)
        {
            if (currentDialog[dialogNumber].name.Contains("Witch"))
            {
                dialogueBox.transform.parent.SetParent(witch.transform);
                dialogueBox.transform.parent.transform.position.Set(0.30f, 1.3f, 0f);
            }

            if (currentDialog[dialogNumber].name.Contains("Princess"))
            {
                dialogueBox.transform.parent.SetParent(princess.transform);
                dialogueBox.transform.parent.transform.position.Set(-0.25f, 1.3f, 0f);
            }

            if (dialogNumber == 12 && currentDialog == firstDialog)
            {
                spawner.GetComponent<SpawnerController>().SpawnInitial();
            }

            if (dialogNumber == 2 && currentDialog == secondDialog)
            {
                GameObject[] objs;
                objs = GameObject.FindGameObjectsWithTag("badObject");
                GameObject[] objs02;
                objs02 = GameObject.FindGameObjectsWithTag("goodObject");

                foreach (GameObject obj in objs)
                {
                    Destroy(obj);
                }

                foreach (GameObject obj in objs02)
                {
                    Destroy(obj);
                }

            }

            if(dialogNumber == 4 && currentDialog == secondDialog)
            {
                FindObjectOfType<SceneController>().GetComponent<SceneController>().SecondRoom();
            }

            dialogueBox.GetComponent<Image>().sprite = currentDialog[dialogNumber];
            dialogNumber++;
        }
        else
        {
            dialogIsOn = false;
            dialogNumber = 0;
            dialogueBox.GetComponent<Image>().sprite = null;
            dialogueBox.SetActive(false);
            StartCoroutine("WaitForCanBubble");
        }
    }

    IEnumerator WaitForCanBubble()
    {
        yield return new WaitForEndOfFrame();
        princess.GetComponent<PlayerController>().ChangeCanBubble(true);
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("badObject");
        GameObject[] objs02;
        objs02 = GameObject.FindGameObjectsWithTag("goodObject");

        foreach (GameObject obj in objs)
        {
            obj.GetComponent<EnemyController>().SetCanMove(true);
        }

        foreach (GameObject obj in objs02)
        {
            obj.GetComponent<EnemyController>().SetCanMove(true);
        }
    }
}
