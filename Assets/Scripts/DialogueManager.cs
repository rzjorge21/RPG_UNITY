using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    public List<Dialogue> dialogues;
    public GameObject dialogueCanvas;
    public PlayerController playerController;

    private Image characterImage;
    private TextMeshProUGUI dialogueText;

    private Dialogue currentDialogue;
    private int currentDialoguePosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            characterImage = dialogueCanvas.transform
                .Find("Panel")
                .Find("CharacterImage").GetComponent<Image>();
            dialogueText = dialogueCanvas.transform
                .Find("Panel")
                .Find("DialogueText").GetComponent<TextMeshProUGUI>();
        }
    }

    public void StartDialogue(int dialogueId)
    {
        currentDialogue = dialogues[dialogueId];
        dialogueCanvas.SetActive(true);

        currentDialoguePosition = 0;

        NextInteraction();

    }

    public void NextInteraction()
    {
        if (currentDialoguePosition < currentDialogue.interactions.Count)
        {
            Interaction interaction = currentDialogue.interactions[currentDialoguePosition++];
            ShowInteractionOnCanvas(interaction);
        }else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        currentDialogue = null;
        currentDialoguePosition = 0;
        playerController.CanMove = true;
        dialogueCanvas.SetActive(false);
    }

    private void ShowInteractionOnCanvas(Interaction interaction)
    {
        characterImage.sprite = 
            interaction.character.GetComponent<SpriteRenderer>().sprite;
        dialogueText.SetText(interaction.text);
    }
}
