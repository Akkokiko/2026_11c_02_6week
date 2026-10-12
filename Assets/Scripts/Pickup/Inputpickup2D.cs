using UnityEngine;

public class InteractPickup2D : MonoBehaviour
{
    private bool playerNearby = false;

    private virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            Debug.Log("Press 'E' to pick up");
        }
    }

    private virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}