using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public Team team;

    public int manaCost;
    public float attack;
    public float attackSpeed;
    public int moveSpeed;
    public int health;
    public float cooldown;

    public RuntimeAnimatorController runtimeAnimatorController;
}
