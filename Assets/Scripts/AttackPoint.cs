using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public CharacterUnit characterUnit;

    public LayerMask enemyLayers;

    private Collider2D closestEnemy;

    private IAttackable attackable;

    private void OnEnable()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, characterUnit.attackRange, enemyLayers);
        
        if (characterUnit.attackType.Equals(AttackType.Area))
            foreach (Collider2D enemy in hitEnemies)
            {
                attackable = enemy.GetComponent<IAttackable>();
                if (attackable != null && !attackable.GetTeam().Equals(characterUnit.team))
                {
                    Hit(attackable);
                }
            }
        else
        {
            

            //if (!hitEnemies.Contains(closestEnemy)) //if need to fight to death with recent enemy
            closestEnemy = hitEnemies.Where(n => n.GetComponent<IAttackable>() != null && !n.GetComponent<IAttackable>().GetTeam().Equals(characterUnit.team))
            .OrderBy(n => (n.transform.position - characterUnit.transform.position).sqrMagnitude)
            .FirstOrDefault();

            if (closestEnemy == null)
                return;

            Hit(closestEnemy.GetComponent<IAttackable>());
        }

    }

    private void Hit(IAttackable attackable)
    {
        characterUnit.audioController.PlayAttackSound();
        attackable.DealDamage(characterUnit.attack);
    }

    private bool isCloser(Collider2D oldEnemy, Collider2D newEnemy)
    {
        if(characterUnit.transform.position.x - oldEnemy.transform.position.x > characterUnit.transform.position.x - newEnemy.transform.position.x)
            return true;
        else
            return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, characterUnit.attackRange);
    }
}
