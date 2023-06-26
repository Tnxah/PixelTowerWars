using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public int level = 1;

    public Sprite icon;

    public Team team;
    public AttackType attackType;

    public int manaCost;
    public float attack;
    public float attackSpeed;
    public int moveSpeed;
    public int health;
    public float cooldown;
    public float attackRange;
    public float triggerAttackRange = 0.7f;

    public RuntimeAnimatorController runtimeAnimatorController;

    public List<UnitUpgrade> upgrades;
}
