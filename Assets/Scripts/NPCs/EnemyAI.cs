
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private PointPatrol patrol;
    private PlayerDetection detection;
    private EnemyCombat combat;
    private AnimationSwitcher eAnimation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        patrol = GetComponent<PointPatrol>();
        detection = GetComponent<PlayerDetection>();
        combat = GetComponent<EnemyCombat>();
        eAnimation = GetComponent<AnimationSwitcher>();
    }

    private void Update()
    {
        detection.Refresh();

        if (detection.Player != null &&
            detection.IsChasing &&
            detection.DistanceToPlayer <= detection.attackRange &&
            !combat.IsBusy)
        {
            combat.TryStartAttack();
        }

        eAnimation.UpdateMovement(
            !combat.IsBusy &&
            Mathf.Abs(rb.linearVelocity.x) > 0.1f
        );
    }

    private void FixedUpdate()
    {
        if (combat.IsBusy)
        {
            patrol.StopHorizontalMovement();
            return;
        }

        if (detection.Player != null && detection.IsChasing)
        {
            if (detection.DistanceToPlayer > detection.attackRange)
            {
                patrol.MoveTowards(
                    detection.Player.position.x,
                    patrol.chaseSpeed
                );
            }
            else
            {
                patrol.StopHorizontalMovement();
            }
        }
        else
        {
            patrol.Patrol();
        }
    }
}
