using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{

    [Header("Health")] 
    [SerializeField] private int m_maxHealth;
    public int MaxHealth => m_maxHealth;
    [SerializeField]private int m_currentHealth;
    public int CurrentHealth => m_currentHealth;
    
    [Header("Base Stats")]
    [SerializeField] private float m_baseMoveSpeed = 5;
    [SerializeField] private float m_baseJumpForce = 20;
    [SerializeField] private float m_baseWeight = 10;

    [Header("Active Stats")]
    public float m_currentMoveSpeed;
    public float m_currentJumpForce;
    public float m_currentWeight;
    public bool m_shouldBeMoving;
    
    [Header("Entity Components")]
    [SerializeField] private Rigidbody2D m_entityRigidbody;
    public Rigidbody2D EntityRigidbody => m_entityRigidbody;
    
    [Header("Weight")]
    public UnityEvent m_onWeightChanged;
    
    public UnityEvent<Entity> m_OnEntityHealthChanged = new UnityEvent<Entity>();

    //EFFECTS
   private List<Effect> m_activeEffects = new List<Effect>();
   private List<Effect> m_effectsToRemove = new List<Effect>();

   protected virtual void Awake()
   {
        m_entityRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_currentMoveSpeed = m_baseMoveSpeed;    
        m_currentJumpForce = m_baseJumpForce;
        m_currentWeight = m_baseWeight;
        m_currentHealth = m_maxHealth;
        m_shouldBeMoving = true;
   }
   
   

   public virtual void Update()
   {
      updateEffects();
      removeExpiredEffects();
   }

   private void updateEffects()
   {
       foreach (Effect effect in m_activeEffects)
       {
           if (effect.countdown(Time.deltaTime))
           {
               if (!m_effectsToRemove.Contains(effect))
               {
                   m_effectsToRemove.Add(effect);
               }
           }
       }
   }

   private void removeExpiredEffects()
   {
       //Do fun stuff with stats
       foreach (Effect effect in m_effectsToRemove)
       {
           modifyStats(effect,true);
           m_activeEffects.Remove(effect);
       }
       m_effectsToRemove.Clear();
   }

   public virtual void removeAllEffect()
   {
       foreach (Effect effect in m_activeEffects)
       {
           m_effectsToRemove.Add(effect);
       }
       removeExpiredEffects();
   }

   public virtual void applyEffect(Effect effect)
   {
      
       m_activeEffects.Add(effect);
       modifyStats(effect);
   }    

   public void modifyStats(Effect effect, bool remove = false)
   {
       switch (effect.EffectType)
       {
           case EffectType.SPEED_UP:
               m_currentMoveSpeed += ((m_baseMoveSpeed * effect.Strength) * (remove?-1:1));
               break;
           
           case EffectType.JUMP_BOOST:
               m_currentJumpForce += ((m_baseJumpForce * effect.Strength) * (remove?-1:1));
               break;
           
               case EffectType.SIZE_UP:
                   m_currentWeight += (m_baseWeight * effect.Strength)*(remove?-1:1);
               transform.localScale = new Vector3(m_currentWeight/m_baseWeight, m_currentWeight/m_baseWeight, m_currentWeight/m_baseWeight);
               m_onWeightChanged.Invoke();
               break;
           case EffectType.FREEZE:
                m_shouldBeMoving = remove;
                break;
            case EffectType.GRAVITY_REVERSE:
                /*if (remove)
                {
                    
                }
                else
                {*/
                    this.gameObject.GetComponent<Rigidbody2D>().gravityScale = -this.gameObject.GetComponent<Rigidbody2D>().gravityScale;
                //}
                break;

           default:
               Debug.Log("Effect type not implemented");
               break;
       }
   }

   

   public virtual void takeDamage(int damage)
   {
       Debug.Log("Taking damage");
       m_currentHealth -= Mathf.Clamp(damage, 0, m_currentHealth);
       m_OnEntityHealthChanged.Invoke(this);
       if (m_currentHealth <= 0)
       {
           die();
       }
       
   }

   public virtual void heal(int value)
   {
       m_currentHealth += value;
       m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
       m_OnEntityHealthChanged.Invoke(this);
   }

   public virtual void die()
   {
       Destroy(gameObject);
   }

}
   