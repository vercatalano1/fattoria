using PlayFab.MultiplayerModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;


    //public GameObject contButtom;
    public float wordSpeed;
    public bool playerIsClose;

    private void Start()
    {
        dialogueText.text = "";
        StartDialogue();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                //zeroText();
                dialoguePanel.SetActive(false);

            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }

            if (dialogueText.text == dialogue[index])
            {
                // contButtom.SetActive(true);
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index];
            }

           
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(Typing());
    }



   /* public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }*/



    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

   

    public void NextLine()
    {
       // contButtom.SetActive(false);


        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            //zeroText();
            dialoguePanel.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            playerIsClose = false;
            //zeroText();
            dialoguePanel.SetActive(false);

        }
    }
}
