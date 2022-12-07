using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private TextMeshProUGUI moneyText;

    /*************************************
    // USING THIS DURING TESTING
    **************************************/
    public delegate void Test();
    public static event Test OnTesting;     // ---> all scriptable objects should subsrcibe to this event

    [Space(20f)]
    [Header("Game Testing Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - "), Space(10f)]

    public bool Testing;
    //************************************

    private void Awake()
    {
        Instance ??= this;  // checks if Instance is null and if true, passes the value of "this"
        
        if (Testing) // reset the values of Scriptable Objects
        {
            // call the event   ---> all SOs will reset their values
            OnTesting();
        }
    }

    private void Start()
    {
        // update money UI
        UpdateMoneyText();
    }

    public bool CanPay(float amount)
    {
        return playerData.Money >= amount;
    }

    public void Pay(float amount)
    {
        playerData.Money -= amount;

        UpdateMoneyText();
    }
    
    public void Earn(float amount)
    {
        playerData.Money += amount;

        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        //moneyText.text = "Money: " + playerData.Money.ToString("C1");   // dollar currency format with 1 decimal space

        moneyText.text = "Money: " + ToKMB(playerData.Money);   // formating the number
    }

    public static string ToKMB(float num)   // method that formats text
    {
        if (num > 999999999)
        {
            return num.ToString("0,,,.###B");
        }
        else if (num > 999999)
        {
            return num.ToString("0,,.###M");
        }
        else if (num > 999)
        {
            return num.ToString("0,.###k");
        }
        else
        {
            return num.ToString("C1"); // dollar currency format with 1 decimal space
        }
    }

    public static void CheckForUIClick()    // using this method to prevent clicking behind UI elements
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
    }
}
