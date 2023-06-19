using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour
{
    public Unit unit;

    public UnitCombat unitCombat;

    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector]
    public int manaCost;
    [HideInInspector]
    public int moveSpeed;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        manaCost = unit.manaCost;
        moveSpeed = unit.moveSpeed;
        health = unit.health;
        cooldown = unit.cooldown;

        unitCombat.attack = unit.attack;
        unitCombat.attackSpeed = unit.attackSpeed;

        animator.runtimeAnimatorController = unit.runtimeAnimatorController;


    }

    private void FixedUpdate()
    {
        if (enemies.Count <= 0)
        {
            Run();
        }
        else if (enemies.Count > 0)
        {
            unitCombat.Attack();
        }
    }

    private void Update()
    {
        animator.SetFloat("Run", rb.velocity.x);
    }

    private void Run()
    {
        animator.SetBool("Run", true);

        Vector2 direction = (Vector2.right).normalized;
        rb.velocity = direction * moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            rb.velocity = Vector2.zero;
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
