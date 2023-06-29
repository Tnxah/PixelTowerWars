using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    private static string savePath = "/Saves/";

    public static SaveData data;

    public static void DeleteSaves()
    {
        Directory.Delete(String.Concat(Application.persistentDataPath, savePath), true);
    }

    public static SaveData Load()
    {
        string str = String.Concat(Application.persistentDataPath, savePath, "save.rims");
        if (!File.Exists(str))
        {
            Debug.Log("Save file not found");
            ApplyDefault();
            return null;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(str, FileMode.Open);
        data = binaryFormatter.Deserialize(fileStream) as SaveData;
        fileStream.Close();

        ApplyLoad();

        return data;
    }

    private static void ApplyDefault()
    {
        data = new SaveData();

        data.money = 0;
        data.completedlevels = 0;
        data.unitslevels.Add("Goblin", 1);

        ApplyLoad();
    }

    public static void Save()
    {
        CollectData();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string str = String.Concat(Application.persistentDataPath, savePath);
        if (!Directory.Exists(str))
        {
            Directory.CreateDirectory(str);
        }
        if (File.Exists(str))
        {
            File.Delete(str);
        }
        str = String.Concat(str, "save.rims");
        FileStream fileStream = new FileStream(str, FileMode.Create);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    private static void CollectData()
    {
        if (data == null) data = new SaveData();

        data.money = GameManager.instance.money;

        foreach (var unit in GameManager.instance.GetUnits())
        {
            data.unitslevels[unit.name] = unit.level;
        }

        data.completedlevels = GameManager.instance.completedLevels;
    }

    private static void ApplyLoad()
    {
        GameManager.instance.money = data.money;

        foreach (KeyValuePair<string, int> unit in data.unitslevels)
        {
            UnitUpgrader.UpgradeUnit(unit.Key, unit.Value);
        }

        GameManager.instance.completedLevels = data.completedlevels;

        Debug.Log("Saves applied");
    }
}