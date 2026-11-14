using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using Sound;

public class DialogueSys : Interactable
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)]
        public string text;
        public ClipSex voiceover;
        public DialogueExtra[] DialogueExtras = new  DialogueExtra[0];
    }

    [Header("Dymek")]
    public GameObject dialogueBubble;
    public TMP_Text dialogueText;

    [Header("Wiadomości NPC")]
    public DialogueLine[] dialogueLines;
    

    [Header("Ustawienia")]
    public KeyCode interactionKey = KeyCode.E;
    public float extraDisplayTime = 0.5f;

    private bool playerInRange;
    private int currentLine;

    private Coroutine dialogueCoroutine;

    protected override void Start()
    {
        base.Start();
        dialogueBubble.SetActive(false);
    }


    public override void Interact(Player player)
    {
        base.Interact(player);
        StartDialogue(player);
    }

    private void StartDialogue(Player player)
    {
        if (dialogueLines == null ||
            dialogueLines.Length == 0)
        {
            return;
        }
        if (currentLine >= dialogueLines.Length)
        {
            currentLine = 0;
        }

        dialogueCoroutine =
            StartCoroutine(PlayDialogueLine(player));
    }

    private IEnumerator PlayDialogueLine(Player player)
    {
        bool canContinue = true;
        DialogueLine line = dialogueLines[currentLine];

        dialogueText.text = line.text;
        dialogueBubble.SetActive(true);

        

        float duration = 0f;
        if (line.voiceover != null && line.voiceover.clip != null)
        {
            line.voiceover.Play(transform.position);
            duration = line.voiceover.clip.length / Mathf.Max(line.voiceover.pitchRange.x, 0.01f);
        }

        yield return new WaitForSeconds(duration + extraDisplayTime);

        dialogueBubble.SetActive(false);
        foreach (DialogueExtra dialogueExtra in line.DialogueExtras)
        {
            if (!dialogueExtra.DoYourStuff(player))
            {
                canContinue = false;
            }
        }

        if (canContinue)
        {
            currentLine++;
        }

        dialogueCoroutine = null;
    }

    public void ResetDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        dialogueBubble.SetActive(false);

        currentLine = 0;
    }
}
