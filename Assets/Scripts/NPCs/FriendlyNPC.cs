using UnityEngine;

public class FriendlyNPC : Interactable
{
    [TextArea(2, 5)]
    public string dialogue =
        "By ze mną porozmawiac naciśnij przycisk 'E'";

    public override void Interact(Player player)
    {
        Debug.Log(dialogue);
    }
}
