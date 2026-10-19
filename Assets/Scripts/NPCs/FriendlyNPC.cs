using UnityEngine;

public class FriendlyNPC : MonoBehaviour
{
    [TextArea(2, 5)]
    public string dialogue =
        "Żeby ze mną porozmawiać, naciśnij przycisk 'E'";

    private bool playerNearby = false;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Naciśnij E, aby porozmawiać.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}