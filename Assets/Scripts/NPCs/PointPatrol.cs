
using UnityEngine;

public class PointPatrol : MonoBehaviour
{
    [SerializeField] private EnemyAI m_enemyAI;
    
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float Speed => m_enemyAI.m_currentMoveSpeed;

    [Header("Chase")] public float chaseSpeedMultiplayer = 1.5f;

    private Rigidbody2D rb;
    private Transform targetPoint;
    private GroundChecker groundCheck;
    public AnimationSwitcher eAnimation;

    private Vector2 pointAPosition;
    private Vector2 pointBPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundChecker>();
        eAnimation = GetComponent<AnimationSwitcher>();
    }

    private void Start()
    {
        if (pointA == null)
            pointA = transform.Find("PointA");

        if (pointB == null)
            pointB = transform.Find("PointB");

        if (pointA == null || pointB == null)
        {
            Debug.LogError("EnemyPatrol: Nie znaleziono punktów patrolu!",this);
            enabled = false;
            return;
        }

                pointAPosition = pointA.position;
        pointBPosition = pointB.position;

        targetPoint = pointB;
    }

    public void Patrol()
    {
        if (targetPoint == null)
            return;

        float targetX = targetPoint == pointA
            ? pointAPosition.x
            : pointBPosition.x;

        float difference = targetX - transform.position.x;

        if (Mathf.Abs(difference) < 0.1f)
        {
            SwitchTarget();
            return;
        }

        float direction = Mathf.Sign(difference);

        if (!groundCheck.HasGroundAhead(direction))
        {
            StopHorizontalMovement();
            SwitchTarget();
            return;
        }

        MoveTowards(targetX, Speed);
    }

    public void MoveTowards(float targetX, float speed)
    {
        float difference = targetX - transform.position.x;

        if (Mathf.Abs(difference) < 0.05f)
        {
            StopHorizontalMovement();
            return;
        }

        float direction = Mathf.Sign(difference);

        if (!groundCheck.HasGroundAhead(direction))
        {
            StopHorizontalMovement();
            return;
        }

        //actual movement
            if (Mathf.Abs(rb.linearVelocityX) > speed)
            {
                if (Mathf.Sign(rb.linearVelocityX) == direction)
                {
                    rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, Time.deltaTime*5);
                }
                else
                {
                    rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, rb.linearVelocityX + direction * speed/5, Time.deltaTime);
                }
            }
            else
            {
                rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            }

            eAnimation.FaceDirection(direction);
    }

    public void StopHorizontalMovement()
    {
        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );
    }

    private void SwitchTarget()
    {
        targetPoint = targetPoint == pointA ? pointB : pointA;
    }
}
