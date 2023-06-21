using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    public static void RewardMana(Team team, int mana)
    {
        print(GameManager.gameManager != null);
        print(GameManager.gameManager.tower != null);
        print(GameManager.gameManager.tower.team != null);
        if (!team.Equals(GameManager.gameManager.tower.team))
        
            GameManager.gameManager.AddMana(mana);
         else
            EnemyManager.enemyManager.AddMana(mana);
    }
}
