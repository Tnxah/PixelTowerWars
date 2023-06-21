using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    public Tower tower;

    private int mana;

    private int manaPerSecond;
    private int maxMana;

    public Transform unitsHolder;
    public GameObject unitButtonPrefab;

    public TowerUnit towerUnit;
    private Unit[] units;


    private void Awake()
    {
        this.units = towerUnit.units;

        this.maxMana = towerUnit.maxMana;
        this.manaPerSecond = towerUnit.manaPerSecond;
    }

    public void Start()
    {
        if (enemyManager == null)
        {
            enemyManager = this;
        }

        StartCoroutine(PerSecond());

        tower.Initialize(towerUnit);
    }

    private void GainMana()
    {
        if (mana < maxMana)
            AddMana(manaPerSecond);
    }

    public void AddMana(int mana)
    {
        this.mana += mana;
        this.mana = Mathf.Clamp(this.mana, 0, maxMana);
    }

    private IEnumerator PerSecond()
    {
        while (true)
        {
            GainMana();

            yield return new WaitForSeconds(1);
        }
    }

    public void SpawnUnit(int index)
    {
        var unit = units[index];

        if (mana < unit.manaCost) return;

        mana -= unit.manaCost;

        tower.SpawnUnit(unit);
    }

    private void FixedUpdate()
    {
        SpawnUnit(0);
    }
}
