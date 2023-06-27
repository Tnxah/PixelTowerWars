using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    private static string savePath = "/Saves/save";

    public static SaveData data;

    public static void DeleteSaves()
    {
        Directory.Delete(String.Concat(Application.persistentDataPath, savePath), true);
    }

    public static SaveData Load()
    {
        string str = String.Concat(Application.persistentDataPath, savePath, ".rims");
        if (!File.Exists(str))
        {
            Debug.Log("Save file not found");
            return null;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(str, FileMode.Open);
        data = binaryFormatter.Deserialize(fileStream) as SaveData;
        fileStream.Close();

        ApplyLoad();

        return data;
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
        str = String.Concat(str, ".rims");
        FileStream fileStream = new FileStream(str, FileMode.Create);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    private static void CollectData()
    {
        if (data == null) data = new SaveData();

        data.money = GameManager.instance.GetMoney();

        foreach (var unit in GameManager.instance.GetUnits())
        {
            data.unitslevels[unit.name] = unit.level;
        }
    }

    private static void ApplyLoad()
    {
        GameManager.instance.SetMoney(data.money);

        foreach (KeyValuePair<string, int> unit in data.unitslevels)
        {
            UnitUpgrader.UpgradeUnit(unit.Key, unit.Value);
        }

        Debug.Log("Saves applied");
    }
}