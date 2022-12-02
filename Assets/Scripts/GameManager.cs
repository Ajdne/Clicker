using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float money;
    public float Money { get { return money; } set { money = value; } }

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
        }

        // update money UI
        UpdateMoneyText();
    }

    // ************************************STRPATI SAV DATA U SO***************************************

    public void Pay(float amount)
    {
        if(money >= amount)
        {
            money -= amount;

            UpdateMoneyText();
        }
        else print("Not enough money!");
    }


    public void UpdateMoneyText()
    {
        moneyText.text = "Money: " + money.ToString();
    }
}
