using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Tower tower;

    
    [SerializeField]
    protected int mana = 25;

    protected int manaPerSecond;
    protected int maxMana;

    public TowerUnit towerUnit;
    public Unit[] units;


    protected virtual void Awake()
    {
        this.units = towerUnit.units;

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

    protected virtual void PerSecond()
    {
        GainMana();
    }

    public virtual void SpawnUnit(int index)
    {
        var unit = units[index];

        if (mana < unit.manaCost) return;

        mana -= unit.manaCost;

        tower.SpawnUnit(unit);
    }


    protected virtual void Defeat()
    {
        this.enabled = false;
    }
}
