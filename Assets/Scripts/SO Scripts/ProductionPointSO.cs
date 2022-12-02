using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Point SO", menuName = "Scriptable Objects/Production Points")]
public class ProductionPointSO : ScriptableObject
{
    [SerializeField] private bool _isUnlocked = false;
    public bool IsUnlocked { get { return _isUnlocked; } set { _isUnlocked = value; } }

    [SerializeField] private int price = 10;
    public int Price { get { return price; } }

    [SerializeField] private float profit = 1;
    public float Profit { get { return profit; } set { profit = value; } }
}
