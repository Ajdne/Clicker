using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Point SO", menuName = "Scriptable Objects/Production Points")]
public class ProductionPointSO : ScriptableObject
{
    [SerializeField] private bool isUnlocked = false;
    public bool IsUnlocked { get { return isUnlocked; } set { isUnlocked = value; } }

    [SerializeField] private float setPrice = 100;
    private float price;
    public float Price { get { return price; } set { price = value; } }

    private int _upgradeLevel = 0;
    public int UpgradeLevel { get { return _upgradeLevel; } set { _upgradeLevel = value; } }

    private int _upgradeModel;  // saves the index of the active model
    public int UpgradeModel { get { return _upgradeModel; } set { _upgradeModel = value; } }

    [SerializeField] private float setProfitValue = 1;
    private float profitValue;
    public float ProfitValue { get { return profitValue; } }

    private float _profitCoefficient;

    [SerializeField] private float setProfitTime = 5;
    private float profitTime;
    public float ProfitTime { get { return profitTime; } set { profitTime = value; } }

    // function that can be called from context menu
    // used during testing
    public void Reset()
    {
        IsUnlocked = false;
        price = setPrice;
        profitValue = setProfitValue;
        _upgradeLevel = 0;
        _upgradeModel = 0;
        profitTime = setProfitTime;

    }
    private void OnEnable()
    {
        // subscribe to the event
        GameManager.OnTesting += Reset;
    }

    private void OnDisable()
    {
        // unsubscribe to the event
        GameManager.OnTesting -= Reset;
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
        return price += (price * 0.3f * _upgradeLevel);
    }

    private float IncreaseProfitValue()
    {
        // f(x) = log(x)  ---> simulating diminishing returns by making the benefit of upgrade rise logarithmically
        _profitCoefficient = (Mathf.Log10(_upgradeLevel) + 1) * 1.3f;

        return profitValue *= _profitCoefficient;
    }

}
