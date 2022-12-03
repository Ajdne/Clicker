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

    [SerializeField] private float price = 10;
    public float Price { get { return price; } }
    

    [SerializeField] private float profitValue = 1;
    public float ProfitValue { get { return profitValue; } }

    private int _upgradeLevel = 1;
    private float _profitCoefficient;


    [SerializeField] private float profitTime = 5;
    public float ProfitTime { get { return profitTime; } set { profitTime = value; } }

    public void Reset()
    {
        isUnlocked = false;
        price = 10;
        profitValue = 1;
        _upgradeLevel = 1;
        profitTime = 5;
    }

    public void UpgradeProduction()
    {
        _upgradeLevel++;
        IncreaseUpgradePrice();
        IncreaseProfitValue();
    }

    public float IncreaseProfitValue()
    {
        // f(x) = ln(x)  ---> simulating diminishing returns by making the benefit of upgrade rise logarithmically
        _profitCoefficient = Mathf.Log(_upgradeLevel) + 1;

        return profitValue *= _profitCoefficient;
    }

    public float IncreaseUpgradePrice()
    {
        // f(x) = 0.5 * x   ---> linear rise - the benefits of upgrade are lower with higher levels
        return price *= (0.5f * _upgradeLevel);
    }
}
