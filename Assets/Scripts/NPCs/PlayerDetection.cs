
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Detection")]
    public Transform player;
    public float sightRange = 5f;
    public float attackRange = 1.2f;

    public bool IsChasing { get; private set; }
    public float DistanceToPlayer { get; private set; }

    public Transform Player => player;

    public void Refresh()
    {
        if (player == null)
        {
            IsChasing = false;
            DistanceToPlayer = Mathf.Infinity;
            return;
        }

        DistanceToPlayer = Vector2.Distance(
            transform.position,
            player.position
        );

        IsChasing = DistanceToPlayer <= sightRange;
    }
}
