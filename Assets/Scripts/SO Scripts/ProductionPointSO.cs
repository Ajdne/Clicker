using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Point SO", menuName = "Scriptable Objects/Production Points")]
public class ProductionPointSO : UnlockableSO
{
    [SerializeField] private bool isUnlocked = false;
    public bool IsUnlocked { get { return isUnlocked; } set { isUnlocked = value; } }

    [SerializeField] private float price = 10;
    public float Price { get { return price; } set { price = value; } }

    private int _upgradeLevel = 0;
    public int UpgradeLevel { get { return _upgradeLevel; } set { _upgradeLevel = value; } }

    private int _upgradeModel;  // saves the index of the active model
    public int UpgradeModel { get { return _upgradeModel; } set { _upgradeModel = value; } }

    [SerializeField] private float profitValue = 1;
    public float ProfitValue { get { return profitValue; } }

    private float _profitCoefficient;

    [SerializeField] private float profitTime = 5;
    public float ProfitTime { get { return profitTime; } set { profitTime = value; } }

    // function that can be called in context menu
    // used during testing
    public void Reset()
    {
        IsUnlocked = false;
        Price = 10;
        profitValue = 1;
        _upgradeLevel = 0;
        _upgradeModel = 0;
        profitTime = 5;
        
    }

    public void UpgradeProduction()
    {
        _upgradeLevel++;
        IncreaseUpgradePrice();
        IncreaseProfitValue();
    }
    private float IncreaseUpgradePrice()
    {
        // linear rise - the benefits of upgrade are lower with higher levels
        return price += (0.7f * _upgradeLevel);
    }

    private float IncreaseProfitValue()
    {
        // f(x) = log(x)  ---> simulating diminishing returns by making the benefit of upgrade rise logarithmically
        _profitCoefficient = Mathf.Log10(_upgradeLevel) + 1;

        return profitValue += _profitCoefficient;
    }

}
