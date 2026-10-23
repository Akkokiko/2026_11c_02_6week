using UnityEngine;

public class CleansingWaters : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.TryGetComponent(out Entity entity))
        {
            entity.removeAllEffect();
        }
    }
}
