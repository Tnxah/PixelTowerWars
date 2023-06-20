using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public CharacterUnit characterUnit;

    public LayerMask enemyLayers;

    private void OnEnable()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            var attackable = enemy.GetComponent<IAttackable>();
            if (attackable != null && !attackable.GetTeam().Equals(characterUnit.team))
            {
                attackable.DealDamage(characterUnit.attack);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
