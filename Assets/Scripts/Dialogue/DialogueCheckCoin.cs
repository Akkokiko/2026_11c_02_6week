using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Coincounter")]
public class DialogueCheckCoin : DialogueExtra
{
    public int coinThreshold = 10;

    public bool resetCoinsOnPass = true;

    public override bool IsCondition => true;

    public override void OnConditionPassed(Player player)
    {
        if (resetCoinsOnPass)
        {
            player.PlayerInventory.setCoinAmount(0);
        }
    }

    public override bool DoYourStuff(Player player)
    {
        Debug.Log("DoYourStuff " + player.PlayerInventory.m_coinAmount +  " " + coinThreshold);
        return player.PlayerInventory.m_coinAmount >= coinThreshold;
    }
}
