using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyManager : PlayerManager
{
    private void FixedUpdate()
    {
        SpawnUnit(0);
    }
}
