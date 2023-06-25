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

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Awake();
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < units.Length; i++)
        {
            int index = i;

            var button = Instantiate(unitButtonPrefab, unitsHolder);

            button.GetComponent<SpawnButton>().Initialize(this, index);
        }
    }
}
