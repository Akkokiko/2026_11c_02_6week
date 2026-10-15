using UnityEngine;


[CreateAssetMenu(fileName = "Potion", menuName = "PotionTest")]
public class Potion : ScriptableObject
{
    [SerializeField] private string m_potionName;
    [SerializeField] private Sprite m_potionSprite; 
    [SerializeField] private Sprite m_effectSprite;
    
    public Sprite PotionSprite => m_potionSprite;
    
    
    [Header("Effect")]
    public EffectType m_effectType;
    [SerializeField] private float m_duration;
    public float Duration => m_duration;
    [SerializeField] private float m_strength;
    public float Strength => m_strength;
    
    

    public virtual void use(Player player)
    {
        
    }

    public virtual void Affect(Entity entity)
    {
        Effect n = new Effect(m_effectType, m_duration, m_strength,m_effectSprite);
        entity.applyEffect(n);
    }

}
