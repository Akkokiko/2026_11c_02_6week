using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Coincounter")]
public class DialogueCheckCoin : DialogueExtra
{
    public int coinThreshold = 10;

    public override bool DoYourStuff(Player player)
    {
        Debug.Log("DoYourStuff " + player.PlayerInventory.m_coinAmount +  " " + coinThreshold);
        return player.PlayerInventory.m_coinAmount >= coinThreshold;
    }
}
