using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/NextScene")]
public class DialogueSceneTransport : DialogueExtra
{
   
   [SerializeField] SceneAsset nextScene;
   public override bool DoYourStuff(Player player)
   {
      
      
      SceneManager.LoadScene(nextScene.name);

      return true;
   }
}
