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


   public void Awake()
   {
      interactAction = Player.InputActionAsset.FindAction("Interact");
   }
   

   public void Update()
   {
      if (nearbyInteractables.Count > 1)
      {
         sortByDistance();
      }

      if (interactAction.WasPerformedThisFrame())
      {
         interact();
      }
   }

   public void sortByDistance()
   {
      nearbyInteractables.Sort((x1,x2) => Vector2.Distance(transform.position,x1.transform.position).CompareTo( Vector2.Distance(transform.position,x2.transform.position)));
   }

   public void interact()
   {
      if (nearbyInteractables.Count > 0)
      {
         Debug.Log("Interact with: " +  nearbyInteractables[0].gameObject.name);
         nearbyInteractables[0].Interact(Player);
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
   }
}
