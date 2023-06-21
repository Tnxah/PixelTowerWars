using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int mapSize = 60;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


}
