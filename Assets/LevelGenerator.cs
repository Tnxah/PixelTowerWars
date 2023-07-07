using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    GameManager gameManager;
    public void Start()
    {
        GameManager.instance.stageController.stages = GenerateStages();
    }

    public static List<EnemyTowerDifficulty> GenerateStages()
    {
        List<EnemyTowerDifficulty> stages = new List<EnemyTowerDifficulty>();

        // Starting values
        int stageNumber = 0;
        int towerHealth = 1000;
        float manaPerSecond = 1f;
        int maxMana = 300;
        int moneyReward = 200;
        int[] enemyLevels = { 1 };

        while (enemyLevels.Length <= 3 && !AllLevelsReachedMax(enemyLevels, 3))
        {
            stages.Add(new EnemyTowerDifficulty
            {
                stageNumber = stageNumber,
                towerHealth = towerHealth,
                manaPerSecond = manaPerSecond,
                maxMana = maxMana,
                moneyReward = moneyReward,
                enemyLevels = (int[])enemyLevels.Clone()
            });

            // Increase values for the next stage
            stageNumber++;
            towerHealth += 100;
            manaPerSecond += 0.1f;
            moneyReward += 100;

            // Add a new level to enemyLevels
            int lastLevel = enemyLevels[enemyLevels.Length - 1];
            if (lastLevel == 3)
            {
                enemyLevels = new int[] { 1 };
            }
            else
            {
                int[] newLevels = new int[enemyLevels.Length + 1];
                Array.Copy(enemyLevels, newLevels, enemyLevels.Length);
                newLevels[newLevels.Length - 1] = lastLevel + 1;
                enemyLevels = newLevels;
            }

            // After adding a new level, manaPerSecond becomes 1
            if (enemyLevels.Length == 1)
            {
                manaPerSecond = 1f;
            }

            // Reset manaPerSecond if it exceeds 1.9
            if (manaPerSecond > 1.9f)
            {
                manaPerSecond = 1f;
            }
        }

        return stages;
    }

    public static bool AllLevelsReachedMax(int[] enemyLevels, int maxLevel)
    {
        foreach (int level in enemyLevels)
        {
            if (level < maxLevel)
            {
                return false;
            }
        }
        return true;
    }

}
