using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int money;

    public Dictionary<string, int> unitslevels;

    public int completedlevels;

    public SaveData()
    {
        unitslevels = new Dictionary<string, int>();
    }
}