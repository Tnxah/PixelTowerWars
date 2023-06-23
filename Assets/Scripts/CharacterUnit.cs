using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour, IAttackable
{
    public Unit unit;

    [HideInInspector]
    public Team team;


    [HideInInspector]
    //public List<GameObject> enemies = new List<GameObject>();
    public bool enemies;
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
    private bool alive = true;

    //===============HEALTH===============
    //[HideInInspector]
    public float health;
    public bool stunned;

    //===============ATTACK===============
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attack;
    [HideInInspector]
    public AttackType attackType;
    public float attackRange = 1f;
    [HideInInspector]
    public float triggerAttackRange;

    private float lastAttackTime;

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

    private void FixedUpdate()
    {
        if (!enemies && alive && !stunned)
        {
            Run();
        }
        else if (enemies && alive && !stunned)
        {
            Attack();
        }

        FindEnemies();
    }

    private void Update()
    {
        animator.SetFloat("Run", Mathf.Abs(rb.velocity.x));
    }

    private void Run()
    {
        animator.SetBool("Run", true);
        rb.velocity = direction * moveSpeed;
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime < attackSpeed)
            return;

        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    //HEALTH
    public void DealDamage(float damage)
    {
        health -= damage;

        if (Random.Range(0, 100) <= 5) //Critical damage
        {
            health -= damage * 0.5f;
            rb.AddForce(-direction * 100f);
            stunned = true;
            animator.SetTrigger("Hit");
        }

        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            alive = false;
            rb.velocity = Vector2.zero;
            EnemyService.RewardMana(team, manaCost / 2);
            animator.SetTrigger("Death");
            StartCoroutine(Death());
        }

        

    }

    public Team GetTeam()
    {
        return team;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //var attackable = other.GetComponent<IAttackable>();
        //if (attackable == null) return;

        //if (!other.GetComponent<IAttackable>().GetTeam().Equals(team))
        //{
        //    enemies.Add(other.gameObject);
        //    rb.velocity = Vector2.zero;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //var attackable = collision.GetComponent<IAttackable>();
        //if (attackable == null) return;

        //if (!collision.GetComponent<IAttackable>().GetTeam().Equals(team) && enemies.Contains(collision.gameObject))
        //{
        //    enemies.Remove(collision.gameObject);
        //}
    }

    public void UnStun()
    {
        stunned = false;
        //rb.velocity = Vector2.zero;
    }

    public IEnumerator Death() {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }

    private void FindEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange * triggerAttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {

            var attackable = enemy.GetComponent<IAttackable>();
            if (attackable != null && !attackable.GetTeam().Equals(team))
            {
                rb.velocity = Vector2.zero;
                enemies = true;
                return;
            }
        }
        enemies = false;
    }
}
