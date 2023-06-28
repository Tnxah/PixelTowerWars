using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerManager : MonoBehaviour
{
    public Tower tower;

    
    [SerializeField]
    protected int mana = 25;

    protected int manaPerSecond;
    protected int maxMana;

    public TowerUnit towerUnit;
    public List<Unit> units;

    public delegate void OnMana();
    public OnMana onManaChanged;


    protected virtual void Awake()
    {
        if (towerUnit.team.Equals(Team.Evil))
        {
            FillUnits(GameManager.instance.GetUnits());
        }
        else
        {
            FillUnits(GameManager.instance.GetEnemies());
        }

        this.maxMana = towerUnit.maxMana;
        this.manaPerSecond = towerUnit.manaPerSecond;
    }

    protected virtual void Start()
    {
        StartCoroutine(PerSecondCoroutine());

        tower.Initialize(towerUnit);
    }

    protected virtual void GainMana()
    {
        if (mana < maxMana)
            AddMana(manaPerSecond);
    }

    public virtual void AddMana(int mana)
    {
        this.mana += mana;
        this.mana = Mathf.Clamp(this.mana, 0, maxMana);

        onManaChanged?.Invoke();
    }

    public virtual void DecreaseMana(int mana)
    {
        this.mana -= mana;

        onManaChanged?.Invoke();
    }
    public int GetMana()
    {
        return mana;
    }

    private IEnumerator PerSecondCoroutine()
    {
        while (true)
        {
            PerSecond();

            yield return new WaitForSeconds(1);
        }
    }

    private void FillUnits(List<Unit> unitList)
    {
        foreach (var unit in unitList)
        {
            if (unit.level > 0)
                units.Add(unit);
        }
    }

    protected virtual void PerSecond()
    {
        GainMana();
    }

    public virtual void SpawnUnit(int index)
    {
        var unit = units[index];

        if (mana < unit.manaCost) return;

        DecreaseMana(unit.manaCost);

        tower.SpawnUnit(unit);
    }


    protected virtual void Defeat()
    {
        this.enabled = false;
    }
}
