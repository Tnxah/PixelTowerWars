using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Tower tower;

    public static GameManager gameManager;

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

        InitializeButtons();
    }

    public void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
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
        print(index);
        var unit = units[index];

        if (mana < unit.manaCost) return;

        mana -= unit.manaCost;

        tower.SpawnUnit(unit);
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < units.Length; i++)
        {
            int index = i;

            var button = Instantiate(unitButtonPrefab, unitsHolder);
            button.GetComponent<Button>().onClick.AddListener(() => SpawnUnit(index));
            button.GetComponentInChildren<Image>().sprite = null;
            button.GetComponentInChildren<TextMeshProUGUI>().text = units[i].manaCost.ToString();
        }
    }
}
