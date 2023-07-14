using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    private int upgradeCost = 50;

    private PlayerManager playerManager;
    private Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        playerManager = MainPlayerManager.instance;
    }

    public void Upgrade()
    {
        playerManager.DecreaseMana(upgradeCost);
        playerManager.IncreaseManaPerSecond();
        upgradeCost *= 2;
        button.interactable = false;
    }

    private void FixedUpdate()
    {
        if (playerManager.GetMana() >= upgradeCost)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
