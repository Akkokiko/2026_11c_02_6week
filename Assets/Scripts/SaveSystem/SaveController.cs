using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SaveGame(int coinAmount, string checkpointID)
    {
        SaveData data = new SaveData();

        data.CoinAmount = coinAmount;
        data.CheckpointID = checkpointID;

        SimpleSaveSystem.SaveXML(data);

        Debug.Log("Gra zapisana!");
    }

    public SaveData LoadGame()
    {
        return SimpleSaveSystem.LoadXML();
    }
}
