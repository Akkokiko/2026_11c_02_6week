using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    
    [SerializeField] private  Rigidbody2D m_rigidBody;
    public Rigidbody2D rigidBody => m_rigidBody;
    
    
    [SerializeField] private  SpriteRenderer m_renderer;

    private Potion m_potionCarried;
    
    //private  Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Lauch(Vector3 direction,Potion potion, float force =  5 )
    {
        m_rigidBody.AddForce(direction*force, ForceMode2D.Impulse);
        m_potionCarried = potion;
        m_renderer.sprite = m_potionCarried.PotionSprite;
    }

    private void Update()
    {
        m_renderer.transform.Rotate(new Vector3(0,0,180*3)*Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Entity>(out Entity e))
        {
            m_potionCarried.Affect(e);
        }

        Destroy(gameObject);
    }
}
