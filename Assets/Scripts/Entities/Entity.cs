using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{

    [Header("Health")] 
    [SerializeField] private int m_maxHealth;
    private int m_currentHealth;
    
    [Header("Base Stats")]
    [SerializeField] private float m_baseMoveSpeed = 5;
    [SerializeField] private float m_baseJumpForce = 20;

    [Header("Active Stats")]
    public float m_currentMoveSpeed;
    public float m_currentJumpForce;
    
    
    //EFFECTS
   private List<Effect> m_activeEffects = new List<Effect>();
   private List<Effect> m_effectsToRemove = new List<Effect>();

   void Awake()
   {
       m_currentMoveSpeed = m_baseMoveSpeed;    
       m_currentJumpForce = m_baseJumpForce;
       m_currentHealth = m_maxHealth;
   }
   
   

   public void Update()
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
               m_effectsToRemove.Add(effect);
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
               m_currentMoveSpeed += ((m_baseMoveSpeed * effect.Strength) * (remove?-1:1));
               break;
           
           default:
               Debug.Log("Effect type not implemented");
               break;
       }
   }

   public virtual void takeDamage(int damage)
   {
       m_currentHealth -= damage;
       if (m_currentHealth <= 0)
       {
           die();
       }
   }

   public virtual void heal(int value)
   {
       m_currentHealth += value;
       m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
   }

   public virtual void die()
   {
       Destroy(gameObject);
   }

}
   