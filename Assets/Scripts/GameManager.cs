using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int _money;

    public int money
    {
        get { return _money; }
        set 
        { 
            _money = value;
            onMoneyChangedCallback?.Invoke();
        }
    }

    public delegate void OnMoney();
    public OnMoney onMoneyChangedCallback;


    private List<Unit> units = new List<Unit>();
    private List<Unit> enemyUnits = new List<Unit>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            LoadUnits();
            SaveLoad.Load();
        }

        DontDestroyOnLoad(gameObject);

        money = 1200;
    }

    private void LoadUnits()
    {
        foreach (Object loadedObject in Resources.LoadAll("Evil/"))
        {
            if (loadedObject is Unit unit)
            {
                units.Add(unit);
            }
        }

        foreach (Object loadedObject in Resources.LoadAll("Humans/"))
        {
            if (loadedObject is Unit unit)
            {
                enemyUnits.Add(unit);
            }
        }
    }

    public List<Unit> GetUnits()
    {
        return units;
    }
    public List<Unit> GetEnemies()
    {
        return enemyUnits;
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
