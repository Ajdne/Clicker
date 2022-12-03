using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data SO", menuName = "Scriptable Objects/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [SerializeField] private float money;
    public float Money { get { return money; } set { money = value; } }

    public void Reset()
    {
        money = 1000;
    }
}
