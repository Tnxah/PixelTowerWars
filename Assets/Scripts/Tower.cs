using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public int health = 1000;
    
    public Team team;

    public Unit unit;
    public GameObject prefab;

    public float lastSpawnTime;

    public void DealDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }

    }

    public Team GetTeam()
    {
        return team;
    }

    private void FixedUpdate()
    {
        if (Time.time - lastSpawnTime >= unit.cooldown)
        {
            var character = Instantiate(prefab);
            character.GetComponent<CharacterUnit>().Initialize(unit, (int)transform.lossyScale.x);
            lastSpawnTime = Time.time;
        }
    }
}
