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

    public void Initialize(PlayerManager playerManager, int unitIndex)
    {
        GetComponent<Button>().interactable = false;

        this.playerManager = playerManager;
        this.unitIndex = unitIndex;

        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Spawn()));
        GetComponentsInChildren<Image>()[1].sprite = playerManager.units[unitIndex]?.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = playerManager.units[unitIndex].manaCost.ToString();
    }

    private IEnumerator Spawn()
    {
        playerManager.SpawnUnit(unitIndex);
        GetComponent<Button>().interactable = false;
        isReady = false;
        yield return new WaitForSeconds(playerManager.units[unitIndex].cooldown);
        GetComponent<Button>().interactable = true;
        isReady = true;
    }

    private void FixedUpdate()
    {
        if (playerManager.GetMana() >= playerManager.units[unitIndex].manaCost && isReady)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
