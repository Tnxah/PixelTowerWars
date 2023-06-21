using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerManager : PlayerManager
{
    public Transform unitsHolder;
    public GameObject unitButtonPrefab;

    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
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
