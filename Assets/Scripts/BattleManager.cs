using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int mapSize = 60;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


}
