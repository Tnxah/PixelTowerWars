using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerUnit : ScriptableObject
{
    public new string name;

    public Team team;

    public int towerHealth;
    public int manaPerSecond;
    public int maxMana;

    public RuntimeAnimatorController runtimeAnimatorController;
}

