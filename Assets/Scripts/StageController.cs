using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static StageController instance;

    public List<EnemyTowerDifficulty> stages = new List<EnemyTowerDifficulty>();

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
