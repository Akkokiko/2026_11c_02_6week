using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject[] m_potionIcons;
   
   
   //Flowers
   [Header("Flowers")]
   public TextMeshProUGUI m_flowerCountBlue;
   public TextMeshProUGUI m_flowerCountSeth;
   public TextMeshProUGUI m_flowerCountGreen;

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
