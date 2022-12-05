using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This So is used to store timer modifier data only - some objects modify it (upgrade points), others use it (production points)
/// 
/// Only 1 instance of this SO is needed
/// </summary>

[CreateAssetMenu(fileName = "Time Modifier SO", menuName = "Scriptable Objects/Time Modifier")]
public class TimeModifierSO : ScriptableObject
{
    [SerializeField] private float timeModifierValue = 1;
    public float TimeModifierValue { get { return timeModifierValue; } set { timeModifierValue = value; } }

    private void Reset()
    {
        timeModifierValue = 1;
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
}
