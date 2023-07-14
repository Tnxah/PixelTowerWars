using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int mapSize = 60;

    public List<GameObject> units = new List<GameObject>();

    public Tower mainPlayerTower;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (units.Count > 1)
                AdsManager.instance.ShowInterstitial();
            
                SceneManager.LoadScene(0);
        }
    }

}
