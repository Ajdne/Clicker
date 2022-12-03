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

    [SerializeField] private int price = 10;
    public int Price { get { return price; } }

    [SerializeField] private float profitValue = 1;
    public float ProfitValue { get { return profitValue; } set { profitValue = value; } }

    [SerializeField] private float profitTime = 5;
    public float ProfitTime { get { return profitTime; } set { profitTime = value; } }

    public void Reset()
    {
        isUnlocked = false;
        price = 10;
        profitValue = 1;
        profitTime = 5;
    }
}
