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

    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float lastAttackTime;

    private void Start()
    {
        characterUnit = GetComponent<CharacterUnit>();
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime < attackSpeed) 
            return;
        
        characterUnit.animator.SetTrigger("Attack");
        lastAttackTime = Time.time;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.5f);

        foreach (Collider2D enemy in hitEnemies)
        {
            print("Hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint)
        {
            Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
        }
    }
}
