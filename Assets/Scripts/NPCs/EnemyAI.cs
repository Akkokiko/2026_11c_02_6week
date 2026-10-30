
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : Entity
{
    private Rigidbody2D rb;
    private PointPatrol patrol;
    private PlayerDetection detection;
    private EnemyCombat combat;
    private AnimationSwitcher eAnimation;

    protected override void Awake()
    {
        base.Awake();
        rb = this.EntityRigidbody;

        patrol = GetComponent<PointPatrol>();
        detection = GetComponent<PlayerDetection>();
        combat = GetComponent<EnemyCombat>();   
        eAnimation = GetComponent<AnimationSwitcher>();
    }

    public override void Update()
    {
        base.Update();
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
        if (!this.m_shouldBeMoving)
        {
            rb.linearVelocity = new Vector2(0,0);
            return;
        }

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
