using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public StageController stageController;
    public EnemyTowerDifficulty difficulty;

    public int completedLevels;

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


    public List<Unit> units = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            LoadUnits();
            SaveLoad.Load();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                Application.Quit();
            else
                SceneManager.LoadScene(0);
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
        var unit = GameManager.instance.GetUnits().Find(unit => unit.name.Equals(name));
        if (unit == null) return;
        
        var upgrade = unit.upgrades.Find(up => up.level == level);
        if (upgrade == null) return;

        unit.attack = upgrade.attack;
        unit.moveSpeed = upgrade.moveSpeed;
        unit.cooldown = upgrade.cooldown;
        unit.manaCost = upgrade.manaCost;
        unit.health = upgrade.health;
        unit.level = upgrade.level;
        unit.attackType = upgrade.attackType;
    }

    public static void UpgradeEnemyUnit(string name, int level)
    {
        var unit = GameManager.instance.GetEnemies().Find(unit => unit.name.Equals(name));
        if (unit == null) return;
        
        var upgrade = unit.upgrades.Find(up => up.level == level);
        if (upgrade == null) return;

        unit.attack = upgrade.attack;
        unit.moveSpeed = upgrade.moveSpeed;
        unit.cooldown = upgrade.cooldown;
        unit.manaCost = upgrade.manaCost;
        unit.health = upgrade.health;
        unit.level = upgrade.level;
        unit.attackType = upgrade.attackType;
    }
}
