using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data SO", menuName = "Scriptable Objects/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [SerializeField] private float money;
    public float Money { get { return money; } set { money = value; } }

    // function that can be called from context menu
    // used during testing
    public void Reset()
    {
        money = 1000;
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
