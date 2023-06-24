using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour, IAttackable
{
    public Unit unit;

    [HideInInspector]
    public Team team;

    public CharacterAnimationController characterAnimationController;

    //[HideInInspector]
    //public List<GameObject> enemies = new List<GameObject>();

    [HideInInspector]
    public int manaCost;
    [HideInInspector]
    public int moveSpeed;
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector2 direction = Vector2.right; //set while spawn
    [HideInInspector]
    public bool alive = true;

    //===============HEALTH===============
    //[HideInInspector]
    public float health;
    public bool stunned;

    //===============ATTACK===============
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public float attack;
    [HideInInspector]
    public AttackType attackType;
    public float attackRange = 1f;
    [HideInInspector]
    public float triggerAttackRange;


    // Start is called before the first frame update
    public void Initialize(Unit unit, int direction)
    {
        this.unit = unit;

        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        
        this.team = unit.team;
        this.manaCost = unit.manaCost;
        this.moveSpeed = unit.moveSpeed;
        this.health = unit.health;
        this.cooldown = unit.cooldown;
        this.direction *= direction;
        this.transform.localScale = new Vector3(
            direction, 
            transform.localScale.y,
            transform.localScale.z
            );

        this.attack = unit.attack;
        this.attackSpeed = unit.attackSpeed;
        this.attackType = unit.attackType;
        this.attackRange = unit.attackRange;
        this.triggerAttackRange = unit.triggerAttackRange;
        
        this.animator.runtimeAnimatorController = unit.runtimeAnimatorController;
    }

    private void Update()
    {
        Run();
        Attack();
    }

    private void Run()
    {
        if (!isAttacking && alive && !stunned)
        {
            rb.velocity = direction * moveSpeed;
        }
    }



    //HEALTH

    public void DealDamage(float damage)
    {
        health -= damage;

        if (Random.Range(0, 100) <= 5) //Critical damage
        {
            health -= damage * 0.5f;
            StartCoroutine(Stun());
        }

        if (health <= 0)
        {
            characterAnimationController.Death();
            GetComponent<BoxCollider2D>().enabled = false;
            alive = false;
            rb.velocity = Vector2.zero;
            EnemyService.RewardMana(team, manaCost / 2);
            StartCoroutine(Death());
        }
    }

    public Team GetTeam()
    {
        return team;
    }

    public IEnumerator Death() {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange * triggerAttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {

            var attackable = enemy.GetComponent<IAttackable>();
            if (attackable != null && !attackable.GetTeam().Equals(team) && alive && !stunned)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero;
                return;
            }
        }
        isAttacking = false;
    }

    private IEnumerator Stun()
    {
        characterAnimationController.Hit();
        stunned = true;
        rb.AddForce(-direction * 100f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        stunned = false;
    }
}
