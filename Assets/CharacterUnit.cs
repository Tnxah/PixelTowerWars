using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour
{
    public Unit unit;

    private List<GameObject> enemies;

    private int manaCost;
    private int attack;
    private int attackSpeed;
    private int moveSpeed;
    private int health;
    private float cooldown;

    private bool isRunning;
    private bool isAttacking;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        manaCost = unit.manaCost;
        attack = unit.attack;
        attackSpeed = unit.attackSpeed;
        moveSpeed = unit.moveSpeed;
        health = unit.health;
        cooldown = unit.cooldown;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemies.Count <= 0)
        {
            Run();
        }
        else if (enemies.Count > 0)
        {
            AttackEnemy();
        }
    }

    private void Run()
    {
            Vector2 direction = (Vector2.right).normalized;
            rb.velocity = direction * moveSpeed;

            // Update the animation parameters for running
            //animator.SetFloat("MoveX", direction.x);
            //animator.SetFloat("MoveY", direction.y);
            //animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void AttackEnemy()
    {
        // Perform the attack logic here
        // For example, reduce the enemy's health or apply damage

        // Uncomment the line below to apply damage to the enemy
        // enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            //isRunning = false;
            //isAttacking = true;
            rb.velocity = Vector2.zero; // Stop the warrior's movement
            //animator.SetFloat("Speed", 0f); // Set the animation speed to 0
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && enemies.Contains(collision.gameObject))
        {
            enemies.Remove(collision.gameObject);
        }
    }


}
