using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money;

    private List<Unit> units;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        units = new List<Unit>();

        foreach (Object loadedObject in Resources.LoadAll("Evil/"))
        {
            if (loadedObject is Unit unit)
            {
                units.Add(unit);
            }
        }
        SaveLoad.Load();
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    public void SetMoney(int amount)
    {
        this.money = amount;
    }

    public int GetMoney()
    {
        return money;
    }

    private void OnApplicationQuit()
    {
        SaveLoad.Save();
    }
}

public class UnitUpgrader
{
    public static void UpgradeUnit(string name, int level)
    {

        Debug.Log(name + " " + level);

        var unit = GameManager.instance.GetUnits().Find(unit => unit.name.Equals(name));
        if (unit == null) return;
        
        var upgrade = unit.upgrades.Find(up => up.level == level);
        if (upgrade == null) return;

        Debug.Log(upgrade.damage);

        unit.attack = upgrade.damage;
        unit.moveSpeed = upgrade.moveSpeed;
        unit.cooldown = upgrade.cooldown;
        unit.manaCost = upgrade.manaCost;
        unit.health = upgrade.health;
        unit.level = upgrade.level;
    }
}
