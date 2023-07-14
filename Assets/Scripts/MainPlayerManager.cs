using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerManager : PlayerManager
{
    public static PlayerManager instance;

    public Transform unitsHolder;
    public GameObject unitButtonPrefab;

    public TextMeshProUGUI manaText;



    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        FillUnits();

        base.Awake();

        onManaChanged += RefreshManaText;
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < units.Count; i++)
        {
            int index = i;

            var button = Instantiate(unitButtonPrefab, unitsHolder);

            button.GetComponent<SpawnButton>().Initialize(index);
        }
    }

    protected override void PerSecond()
    {
        base.PerSecond();
    }

    public void RefreshManaText()
    {
        manaText.text = Mathf.Round(mana).ToString();
    }

    private void FillUnits()
    {
        foreach (var unit in GameManager.instance.GetUnits())
        {
            if (unit.level > 0)
                units.Add(unit);
        }
    }
}
