using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private TextMeshProUGUI moneyText;

    /*************************************
    // USING THIS DURING TESTING
    **************************************/
    [Space(20f)]
    [Header("Game Testing Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - "), Space(10f)]

    public bool Testing;

    [Space(10f)]
    [SerializeField] private List<ProductionPointSO> productionPointSOs = new List<ProductionPointSO>();

    //[Header("- - - - - - - - - -  - - - - - - - - - - "), Space(20f)]
    //************************************

    private void Awake()
    {
        Instance ??= this;  // checks if Instance is null and if true, passes the value of "this"
    }

    private void Start()
    {
        if (Testing) // reset the values of Scriptable Objects
        {
            foreach (ProductionPointSO so in productionPointSOs)
            {
                so.Reset();
                //obj.GetComponent<ProductionPoint>().ChangeUnlockedState();
            }

            // playerData.Reset();
        }

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
        moneyText.text = "Money: " + playerData.Money.ToString();
    }
}
