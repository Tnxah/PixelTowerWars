using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour, IAttackable
{
    public Unit unit;

    [HideInInspector]
    public Team team;


    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
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
    public int health;

    //===============ATTACK===============
    [HideInInspector]
    public int attackSpeed;
    [HideInInspector]
    public int attack;

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
        
        this.animator.runtimeAnimatorController = unit.runtimeAnimatorController;
    }

    private void FixedUpdate()
    {
        if (enemies.Count <= 0 && alive)
        {
            Run();
        }
        else if (enemies.Count > 0 && alive)
        {
            Attack();
        }
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
    public void DealDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            alive = false;
            rb.velocity = Vector2.zero;
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

        var attackable = other.GetComponent<IAttackable>();
        if (attackable == null) return;

        if (!other.GetComponent<IAttackable>().GetTeam().Equals(team))
        {
            enemies.Add(other.gameObject);
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var attackable = collision.GetComponent<IAttackable>();
        if (attackable == null) return;

        if (!collision.GetComponent<IAttackable>().GetTeam().Equals(team) && enemies.Contains(collision.gameObject))
        {
            enemies.Remove(collision.gameObject);
        }
    }

    public IEnumerator Death() {
        yield return new WaitForSeconds(5);

        //REWARD;

        Destroy(gameObject);
    }
}
