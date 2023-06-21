using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    public static void RewardMana(Team team, int mana)
    {
        if (!team.Equals(MainPlayerManager.instance.tower.team))

            MainPlayerManager.instance.AddMana(mana);
         else
            EnemyManager.instance.AddMana(mana);
    }
}
