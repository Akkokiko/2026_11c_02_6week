using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject[] m_potionIcons;
   
   [SerializeField] private TextMeshProUGUI m_coinAmount;
   
   
   //Flowers
   [Header("Flowers")]
   public FlowerUICard[] m_flowerCards;

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
   
   
   //Flowers
   public void updateFlowerCard(Flower flower, int newAmount)
   {
       foreach (FlowerUICard card in m_flowerCards)
       {
           if (card.RepresentedFlower == flower)
           {
               card.updateCountText(newAmount);
               return;
           }
       }
   }


   public void updateCoinsText(int newAmount)
   {
       m_coinAmount.text = newAmount.ToString();    
   }



}
