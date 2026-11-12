using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using Sound;

public class DialogueSys : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)]
        public string text;
        public ClipSex voiceover;
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

    private void Start()
    {
        dialogueBubble.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange &&
            dialogueCoroutine == null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void StartDialogue()
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
            StartCoroutine(PlayDialogueLine());
    }

    private IEnumerator PlayDialogueLine()
    {

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

        currentLine++;
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
