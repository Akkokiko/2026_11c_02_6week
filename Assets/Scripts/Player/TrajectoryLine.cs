using System;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerPotionController m_playerPotionController;
    [SerializeField] private Transform m_throwingPoint;

    [Header("Trajectory")] 
    [SerializeField] private int m_segmentCount = 50;
    [SerializeField] private float m_curveLength = 3.5f;


    private Vector2[] m_segments;
    private LineRenderer m_lineRenderer;

    
    private PotionProjectile m_projectile;
    private Rigidbody2D m_projectileRigidBody;

    private const float TIME_CURVE_ADDITION = 0.5f;

    private float m_potionSize;
    
    private void Start()
    {
        m_segments = new Vector2[m_segmentCount];
        m_projectile = m_playerPotionController.PotionProjectile;
        m_projectileRigidBody = m_projectile.GetComponent<Rigidbody2D>();
        
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = m_segmentCount;
        
        m_potionSize = m_projectile.PotionRenderer.bounds.size.x;
    }

    private void FixedUpdate()
    {
        Vector2 startPoint = m_throwingPoint.position;
        m_lineRenderer.positionCount = m_segmentCount;
        m_segments[0] = startPoint;
        
        m_lineRenderer.SetPosition(0, startPoint);
        
        Vector2 startVelocity = m_playerPotionController.ThrowingForce * m_playerPotionController.aimDirection(); 
        
        for (int i = 1; i < m_segmentCount; i++)
        {
            float timeOffset = (i * Time.fixedDeltaTime * m_curveLength);
            
            Vector2 gravityOffset = TIME_CURVE_ADDITION * Physics2D.gravity * m_projectileRigidBody.mass * Mathf.Pow(timeOffset, 2);
            
            m_segments[i] = m_segments[0] + startVelocity * timeOffset +  gravityOffset;  

            RaycastHit2D hit = Physics2D.CircleCast(m_segments[i], m_potionSize, m_playerPotionController.aimDirection(), m_potionSize);
            if (hit.collider != null && hit.collider.isTrigger == false)
            {
                //m_segments[i] = hit.centroid;
                m_lineRenderer.SetPosition(i, m_segments[i]);
                m_lineRenderer.positionCount = i + 1;
                break;
            }

            m_lineRenderer.SetPosition(i, m_segments[i]);
        }
    }
}
