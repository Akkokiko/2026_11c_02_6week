using UnityEngine;

public class SaveController : MonoBehaviour
{
    public void SaveGame()
    {
        SaveData data = new SaveData();

        GameEventSystem.SaveGame(data);

        SimpleSaveSystem.SaveXML(data);
    }

    public void LoadGame()
    {
        SaveData data = SimpleSaveSystem.LoadXML();

        if (data == null)
            return;

        GameEventSystem.LoadGame(data);
    }
}
