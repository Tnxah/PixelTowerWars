using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Random = System.Random;

public class EnemyManager : PlayerManager
{
    public static PlayerManager instance;
    
    private Random rnd = new Random();

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        StartCoroutine(SpawnRandom());
        //SpawnUnit(0);
        //SpawnUnit(1);
    }
    private void FixedUpdate()
    {
        //SpawnUnit(0);
    }

    private IEnumerator SpawnRandom() 
    {
        while (true)
        {
            var randomIndex = rnd.Next(0, units.Length);
            var randomUnit = units[randomIndex];

            yield return new WaitUntil(() => mana >= randomUnit.manaCost);

            SpawnUnit(randomIndex);
        }
        
    }
}
