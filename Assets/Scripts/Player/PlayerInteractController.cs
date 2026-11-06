using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInteractController : MonoBehaviour
{
   [SerializeField] private Player m_player;
   public Player Player => m_player;
   
    public List<Interactable> nearbyInteractables = new List<Interactable>();
   private InputAction interactAction;
   
   public Interactable m_currentlyHoveredInteractable;
   


   public void Awake()
   {
      interactAction = Player.InputActionAsset.FindAction("Interact");
   }
   

   public void Update()
   {
      
      
         sortByDistance();
      

      if (interactAction.WasPerformedThisFrame())
      {
         interact();
      }
   }

   public void sortByDistance()
   {
      if(nearbyInteractables.Count == 0)
         return;
      nearbyInteractables.Sort((x1,x2) => Vector2.Distance(transform.position,x1.transform.position).CompareTo( Vector2.Distance(transform.position,x2.transform.position)));

      if (nearbyInteractables[0] != m_currentlyHoveredInteractable)
      {
         m_currentlyHoveredInteractable = nearbyInteractables[0];
         foreach (Interactable interactable in nearbyInteractables)
         {
            if (interactable != m_currentlyHoveredInteractable)
            {
               interactable.unHover();
            }
            else
            {
               interactable.Hover();
            }
         }
      }

     

      
      
   }

   public void interact()
   {
      if (nearbyInteractables.Count > 0)
      {
         Debug.Log("Interact with: " +  nearbyInteractables[0].gameObject.name);
         m_currentlyHoveredInteractable.Interact(Player);
      }
   }

  

   public void addNearbyInteractable(Interactable interactable)
   {
      if(nearbyInteractables.Contains(interactable))
         return;
      nearbyInteractables.Add(interactable);
   }
   public void removeNearbyInteractable(Interactable interactable)
   {
      if(nearbyInteractables.Contains(interactable))  
         nearbyInteractables.Remove(interactable);
      if(interactable == m_currentlyHoveredInteractable)
         m_currentlyHoveredInteractable = null;
   }
}
