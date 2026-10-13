using UnityEngine;


[CreateAssetMenu(fileName = "Potion", menuName = "PotionTest")]
public class Potion : ScriptableObject
{
    [SerializeField] private string m_potionName;
    [SerializeField] private Sprite m_potionSprite; 
    public Sprite PotionSprite => m_potionSprite;

    public virtual void use(Player player)
    {
        
    }

    public virtual void Affect(Entity entity)
    {
        Debug.Log("Affected: " + entity.name);
    }

}
