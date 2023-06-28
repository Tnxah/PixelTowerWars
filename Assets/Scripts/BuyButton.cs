using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private GameManager gameManager;
    private Unit unit;
    private UnitUpgrade upgrade;
    private Button button;
    [SerializeField]
    private Image unitImage;
    [SerializeField]
    private TextMeshProUGUI upgradeCostText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private GameObject levelBackground;

    public void Initialize(int unitIndex)
    {
        gameManager = GameManager.instance;
        button = GetComponentInChildren<Button>();
        unit = gameManager.GetUnits()[unitIndex];

        button.interactable = false;

        button.onClick.AddListener(() => Buy());
        unitImage.sprite = unit.icon;

        RefreshButton();
    }

    public void RefreshButton()
    {
        if (unit == null) return;

        if (unit.level == 0) levelBackground.SetActive(false);
        else
        {
            levelBackground.SetActive(true);
            levelText.text = unit.level.ToString();
        }

        if (unit.level >= unit.upgrades.Count) {
            upgradeCostText.text = "Sold out";
            return;
        }

        upgrade = unit.upgrades.Find(up => up.level == unit.level + 1);

        if (upgrade == null)
        {
            button.interactable = false;
            return;
        }

        upgradeCostText.text = upgrade.upgradeCost.ToString();

        if(IsEnoughMoney())
            button.interactable = true;
    }

    private void Buy()
    {
        UnitUpgrader.UpgradeUnit(unit.name, upgrade.level);
        gameManager.money -= upgrade.upgradeCost;
        button.interactable = false;

        RefreshButton();
    }

    private bool IsEnoughMoney()
    {
        return gameManager.money >= upgrade.upgradeCost ? true : false;
    }
    private void OnEnable()
    {
        RefreshButton();
    }
}
