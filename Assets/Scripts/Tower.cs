using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public float health = 1000;
    
    public Team team;

    public Unit unit;
    public GameObject prefab;

    public float lastSpawnTime;

    public Transform spawnPoint;

    public void DealDamage(float damage)
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
        var randomPosition = new Vector3(spawnPoint.position.x, UnityEngine.Random.Range(spawnPoint.position.y - 0.3f, spawnPoint.position.y + 0.3f), spawnPoint.position.z);

        if (Time.time - lastSpawnTime >= unit.cooldown)
        {
            var character = Instantiate(prefab, randomPosition, Quaternion.identity);
            character.GetComponent<CharacterUnit>().Initialize(unit, (int)transform.lossyScale.x);
            character.GetComponent<SpriteRenderer>().sortingOrder = (int)(-randomPosition.y * 100f);
            lastSpawnTime = Time.time;
        }
    }
}
