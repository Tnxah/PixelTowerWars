using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    private PlayerManager playerManager;
    private int unitIndex;
    private bool isReady = true;

    public Button button;
    public Image icon;
    public TextMeshProUGUI manaCost;

    private void Awake()
    {
        playerManager = MainPlayerManager.instance;
    }

    public void Initialize(int unitIndex)
    {
        button.interactable = false;

        this.unitIndex = unitIndex;

        button.onClick.AddListener(() => StartCoroutine(Spawn()));
        icon.sprite = playerManager.units[unitIndex]?.icon;
        manaCost.text = playerManager.units[unitIndex].manaCost.ToString();
    }

    private IEnumerator Spawn()
    {
        playerManager.SpawnUnit(unitIndex);
        button.interactable = false;
        isReady = false;
        yield return new WaitForSeconds(playerManager.units[unitIndex].cooldown);
        button.interactable = true;
        isReady = true;
    }

    private void FixedUpdate()
    {
        if (playerManager.GetMana() >= playerManager.units[unitIndex].manaCost && isReady)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
