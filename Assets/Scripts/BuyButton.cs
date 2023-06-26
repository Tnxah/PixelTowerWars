using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private GameManager gameManager;
    private Unit unit;
    private UnitUpgrade up;

    public void Initialize(int unitIndex)
    {
        GetComponent<Button>().interactable = false;

        gameManager = GameManager.instance;

        unit = gameManager.GetUnits()[unitIndex];
        up = unit.upgrades.Find(up => up.level == unit.level + 1);

        GetComponent<Button>().onClick.AddListener(() => Buy());
        GetComponentsInChildren<Image>()[1].sprite = unit?.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = up.upgradeCost.ToString();

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (gameManager.GetMoney() >= up.upgradeCost);
            GetComponent<Button>().interactable = true;
    }

    private void Buy()
    {
        Debug.Log("Buy");

        UnitUpgrader.UpgradeUnit(unit.name, up.level);
        GetComponent<Button>().interactable = false;
        gameManager.SetMoney(gameManager.GetMoney() - up.upgradeCost);
        if (gameManager.GetMoney() >= up.upgradeCost);
            GetComponent<Button>().interactable = true;
    }
}
