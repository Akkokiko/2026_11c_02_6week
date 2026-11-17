using UnityEngine;
using Sound;
public class CleansingWaters : MonoBehaviour
{
    [SerializeField] private Sex clenseSe;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.TryGetComponent(out Entity entity))
        {
            entity.removeAllEffect();
            clenseSe.Play(transform.position);
        }
        
    }
}
