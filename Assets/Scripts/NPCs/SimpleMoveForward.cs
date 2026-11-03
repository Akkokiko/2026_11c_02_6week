using UnityEngine;

public class SimpleMoveForward : MonoBehaviour
{
    
    public AnimationSwitcher eAnimation;
    [SerializeField] private Entity m_entity;
    private Rigidbody2D rb => m_entity.EntityRigidbody;
    [SerializeField] private float m_direction;
    private LayerMask m_groundedMask;
    
    

    private float speed => m_entity.m_currentMoveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_groundedMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, m_groundedMask);
        if (!hit)
        {
            Debug.DrawRay(transform.position, Vector2.left, Color.red);
            transform.Translate(new Vector2(m_direction, 0) * (speed * Time.deltaTime), Space.World);
        }
        else
        {
            //Debug.Log("Hit " + hit.collider.name);
            Debug.DrawRay(transform.position, Vector2.left, Color.green);
        }

        eAnimation.FaceDirection(m_direction);
    }
}
