using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    private CharacterUnit characterUnit;

    [HideInInspector]
    public int attack;
    [HideInInspector]
    public int attackSpeed;

    private float lastAttackTime;

    private void Start()
    {
        characterUnit = GetComponent<CharacterUnit>();
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            characterUnit.animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }

        // For example, reduce the enemy's health or apply damage

        // Uncomment the line below to apply damage to the enemy
        // enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
    }
}
