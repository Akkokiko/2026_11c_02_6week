using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    
    [SerializeField] private  Rigidbody2D m_rigidBody;
    public Rigidbody2D rigidBody => m_rigidBody;
    
    //private  Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Lauch(Vector3 direction)
    {
        m_rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);
    }
}
