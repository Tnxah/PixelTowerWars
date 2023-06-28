using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform buttonsHolder;
    public GameObject buyButtonPrefab;
    public TextMeshProUGUI money;

    public GameObject shop;
    public GameObject credits;
    public GameObject buttons;

    private void Start()
    {
        GameManager.instance.onMoneyChangedCallback += () => money.text = GameManager.instance.money.ToString();

        InitializeShopButtons();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battlefield");
    }

    public void SetButtons(bool state)
    {
        if (state)
        {
            SetShop(false);
        }
        buttons.SetActive(state);
    }

    public void SetShop(bool state)
    {
        if (state)
        {
            SetButtons(false);
            money.text = GameManager.instance.money.ToString();
        }
        shop.SetActive(state);            
    }

    //public void SetCredits(bool state)
    //{
    //    CloseEverithing();
    //    credits.SetActive(state);
    //}

    private void InitializeShopButtons()
    {
        print(GameManager.instance.GetUnits() == null);

        for (int i = 0; i < GameManager.instance.GetUnits().Count; i++)
        {
            int index = i;

            var button = Instantiate(buyButtonPrefab, buttonsHolder);

            button.GetComponent<BuyButton>().Initialize(index);
        }
    }
}