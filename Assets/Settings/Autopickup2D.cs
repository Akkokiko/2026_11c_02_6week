using UnityEngine;

public class AutoPickup2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Item aquired");
            Destroy(gameObject);
        }
    }
}