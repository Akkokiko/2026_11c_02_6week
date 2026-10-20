using UnityEngine;

public class AutoPickup2D : MonoBehaviour
{
    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            pickup(player);
        }
    }

    public virtual void pickup(Player player)
    {
        
    }
}