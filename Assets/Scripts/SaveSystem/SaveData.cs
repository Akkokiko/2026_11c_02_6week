using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int CoinAmount;
    public int DiamondAmount;
    public string CheckpointID;
    public int m_CurrentLevel;
    public List<string> m_recipeList;
    public List<string> CollectedCoins = new List<string>();
}
