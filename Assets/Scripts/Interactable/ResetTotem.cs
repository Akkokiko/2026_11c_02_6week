using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResetTotem : Interactable
{
    public Entity[] m_assignedObjects;
    private Dictionary<Entity, Vector3> m_entityPositions = new Dictionary<Entity, Vector3>();


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
            mEntityPosition.Key.transform.position = mEntityPosition.Value;  
            mEntityPosition.Key.respawn();
        }
    }



}
