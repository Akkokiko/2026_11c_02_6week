using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int CoinAmount;
    public string CheckpointID;
    public int m_CurrentLevel;
    public List<string> CollectedCoins = new List<string>();
}
