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


    private int id;
    public void Initialize(int unitIndex)
    {
        id = unitIndex;

        GetComponent<Button>().interactable = false;

        gameManager = GameManager.instance;

        unit = gameManager.GetUnits()[unitIndex];
        up = unit.upgrades.Find(up => up.level == unit.level + 1);

        if (up == null){
            Destroy(gameObject);
            return; 
        }

        GetComponent<Button>().onClick.AddListener(() => Buy());
        GetComponentsInChildren<Image>()[1].sprite = unit?.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = up.upgradeCost.ToString();

        IsEnoughMoney();

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        IsEnoughMoney();
    }

    private bool IsEnoughMoney()
    {
        var isEnough = gameManager.GetMoney() >= up.upgradeCost ? true : false;

        GetComponent<Button>().interactable = isEnough;
        return isEnough;
    }

    private void Buy()
    {
        UnitUpgrader.UpgradeUnit(unit.name, up.level);
        GetComponent<Button>().interactable = false;
        gameManager.SetMoney(gameManager.GetMoney() - up.upgradeCost);
        if (gameManager.GetMoney() >= up.upgradeCost)
            GetComponent<Button>().interactable = true;
            
        up = unit.upgrades.Find(up => up.level == unit.level + 1);

        if (up == null)
        {
            Destroy(gameObject);
            return;
        }

        GetComponentInChildren<TextMeshProUGUI>().text = up.upgradeCost.ToString();


        if (IsReadyUpdate())
        {
            IsEnoughMoney();
        }

    }

    private bool IsReadyUpdate()
    {
        return unit.level < unit.upgrades.Count ? true : false;
    }
}
