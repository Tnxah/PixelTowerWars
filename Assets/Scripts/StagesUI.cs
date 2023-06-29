using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagesUI : MonoBehaviour
{   
    public TextMeshProUGUI stageNumber;
    public TextMeshProUGUI moneyReward;

    private GameManager gameManager;
    private StageController stageController;

    private EnemyTowerDifficulty currentStage;

    private void Start()
    {
        gameManager = GameManager.instance;
        stageController = GameManager.instance.stageController;

        SetStage(gameManager.completedLevels);
        Refresh();
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battlefield");
    }

    public void Next()
    {
        var number = (currentStage.stageNumber + 1 == stageController.stages.Count) || (currentStage.stageNumber + 1 > gameManager.completedLevels) ? 0 : gameManager.completedLevels;

        SetStage(number);
        Refresh();
    }

    public void Previous()
    {
        var number = currentStage.stageNumber - 1 < 0 ? gameManager.completedLevels : currentStage.stageNumber - 1;

        SetStage(number);
        Refresh();
    }

    private void Refresh()
    {
        stageNumber.text = currentStage.stageNumber.ToString();
        if (currentStage.stageNumber < gameManager.completedLevels)
        {
            moneyReward.enabled = false;
        }
        else
            moneyReward.enabled = true;
        moneyReward.text = currentStage.moneyReward.ToString();
    }

    private void SetStage(int number)
    {
        currentStage = stageController.stages[number];
        gameManager.difficulty = currentStage;
    }
}
