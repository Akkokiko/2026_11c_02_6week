using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/NextScene")]
public class DialogueSceneTransport : DialogueExtra
{
   
   [SerializeField] SceneAsset nextScene;
   public override void DoYourStuff()
   {
      base.DoYourStuff();
      
      SceneManager.LoadScene(nextScene.name);
   }
}
