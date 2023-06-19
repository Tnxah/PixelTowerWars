using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fiendly Unit", menuName = "Fiendly Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public Team team;

    public int manaCost;
    public int attack;
    public int attackSpeed;
    public int moveSpeed;
    public int health;
    public float cooldown;

    public RuntimeAnimatorController runtimeAnimatorController;
}
