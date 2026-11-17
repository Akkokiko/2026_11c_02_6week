    using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueExtra")]
public class DialogueExtra : ScriptableObject 
{
    public virtual bool IsCondition => false;

    public virtual void OnConditionPassed(Player player)
    {
    }

    public virtual bool DoYourStuff(Player player)
    {
        return true;
    }
}
