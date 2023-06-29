using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Random = System.Random;

public class EnemyManager : PlayerManager
{
    public static PlayerManager instance;
    
    private Random rnd = new Random();

    private float[] lastSpawn;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitializeTowerUnit();
        InitializeEnemies();

        FillUnits();
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        lastSpawn = new float[units.Count];
        StartCoroutine(SpawnRandom());
    }

    private void InitializeTowerUnit()
    {
        towerUnit.maxMana = GameManager.instance.difficulty.maxMana;
        towerUnit.manaPerSecond = GameManager.instance.difficulty.manaPerSecond;
        towerUnit.towerHealth = GameManager.instance.difficulty.towerHealth;
    }
    private IEnumerator SpawnRandom() 
    {
        while (true)
        {
            var randomIndex = rnd.Next(0, units.Count);
            var randomUnit = units[randomIndex];

            yield return new WaitUntil(() => mana >= randomUnit.manaCost && Time.time - lastSpawn[randomIndex] >= units[randomIndex].cooldown);

            SpawnUnit(randomIndex);
            lastSpawn[randomIndex] = Time.time;
        }
        
    }

    private void FillUnits()
    {
        foreach (var unit in GameManager.instance.GetEnemies())
        {
            if (unit.level > 0)
                units.Add(unit);
        }
    }

    private void InitializeEnemies()
    {
        var enemyLevels = GameManager.instance.difficulty.enemyLevels;
        var enemies = GameManager.instance.GetEnemies();

        for (int i = 0; i < enemies.Count; i++)
        {
            var level = i < enemyLevels.Length ? enemyLevels[i] : 0;
            UnitUpgrader.UpgradeEnemyUnit(enemies[i].name, level);

            if (i == enemies.Count - 1) return;
        }
    }
}
