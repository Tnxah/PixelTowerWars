using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public float health;
    
    public Team team;

    public GameObject unitPrefab;

    public Transform spawnPoint;

    public Animator animator;

    public void Initialize(TowerUnit unit)
    {
        this.team = unit.team;
        this.health = unit.towerHealth;

        //this.animator.runtimeAnimatorController = unit.runtimeAnimatorController;
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        //animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Death();
        }

    }

    public Team GetTeam()
    {
        return team;
    }

    public void SpawnUnit(Unit unit)
    {
        var randomPosition = new Vector3(spawnPoint.position.x, Random.Range(spawnPoint.position.y - 0.3f, spawnPoint.position.y + 0.3f), spawnPoint.position.z);

        var character = Instantiate(unitPrefab, randomPosition, Quaternion.identity);

        character.GetComponent<CharacterUnit>().Initialize(unit, (int)transform.lossyScale.x);
        character.GetComponent<SpriteRenderer>().sortingOrder = (int)(-randomPosition.y * 100f);
    }

    private void Death()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        if (!team.Equals(MainPlayerManager.instance.tower.team) && GameManager.instance.difficulty.stageNumber == GameManager.instance.completedLevels)
        {
            GameManager.instance.completedLevels++;
        }

        EnemyService.RewardMoney(team, GameManager.instance.difficulty.moneyReward);


        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

        //animator.SetTrigger("Destroy");
    }
}
