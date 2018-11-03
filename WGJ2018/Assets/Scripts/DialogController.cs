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
                dialogueBox.transform.parent.transform.position.Set(0.30f, 0.25f, 0f);
            }

            if (currentDialog[dialogNumber].name.Contains("Princess"))
            {
                dialogueBox.transform.parent.SetParent(princess.transform);
                dialogueBox.transform.parent.transform.position.Set(-0.25f, 0.25f, 0f);
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
        if (currentDialog == firstDialog)
        {
            spawner.GetComponent<SpawnerController>().SpawnInitial();
        }
    }
}
