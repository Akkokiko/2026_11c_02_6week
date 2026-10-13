using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject[] m_potionIcons;

   public void Awake()
   {
      foreach (GameObject potionIcon in m_potionIcons)
      {
          potionIcon.GetComponent<Outline>().enabled = false;
      }
      
   }


   public void chosePotion(int slot)
   {
       foreach (GameObject potionIcon in m_potionIcons)
       {
           potionIcon.GetComponent<Outline>().enabled = false;
       }
       m_potionIcons[slot].GetComponent<Outline>().enabled = true;
   }

   public void updatePotionInventory(Potion[] potions)
   {
       for(int i=0; i<potions.Length; i++)
       {
           Potion potion = potions[i];
           if (null == potion)
           {
               m_potionIcons[i].GetComponent<Image>().sprite = null;
           }
           else
           {
               m_potionIcons[i].GetComponent<Image>().sprite = potion.PotionSprite;
           }
       }
   }
}
