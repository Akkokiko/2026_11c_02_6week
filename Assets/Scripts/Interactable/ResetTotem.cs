using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResetTotem : Interactable
{
    private Entity[] m_assignedObjects;
    private Dictionary<Entity, Vector2> m_entityPositions = new Dictionary<Entity, Vector2>();


    public void Start()
    {
        foreach (var e in m_assignedObjects)
        {
            m_entityPositions.TryAdd(e, e.transform.position);
        }
    }

    public override void Interact(Player player)
    {
        foreach (var mEntityPosition in m_entityPositions)
        {
            mEntityPosition.Key.removeAllEffect();
            mEntityPosition.Key.transform.localPosition = mEntityPosition.Value;    
            mEntityPosition.Key.respawn();
        }
    }



}
