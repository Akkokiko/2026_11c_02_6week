using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/NextScene")]
public class DialogueSceneTransport : DialogueExtra
{

   public override bool DoYourStuff(Player player)
   {
      LevelManager.instance.GoToNextLevel();
      return true;
   }
}
