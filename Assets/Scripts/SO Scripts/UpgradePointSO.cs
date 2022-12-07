using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Point SO", menuName = "Scriptable Objects/Upgrade Points")]
public class UpgradePointSO : UnlockableSO
{
    [SerializeField] private bool isUnlocked = false;
    public bool IsUnlocked { get { return isUnlocked; } set { isUnlocked = value; } }

    private int _upgradeLevel = 0;
    public int UpgradeLevel { get { return _upgradeLevel; } set { _upgradeLevel = value; } }

    [SerializeField] private float setPrice = 1000;
    private float price;
    public float Price { get { return price; } set { price = value; } }

    [SerializeField] private float setTimeCoefficient = 0.05f;
    private float timeCoefficinet;
    public float TimeCoefficient { get { return timeCoefficinet; } set { timeCoefficinet = value; } }

    public void Reset()
    {
        isUnlocked = false;
        _upgradeLevel = 0;
        price = setPrice;
        timeCoefficinet = setTimeCoefficient;
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

    //public float UpgradeTimeCoefficient()
    //{
    //    // f(x) = log(x)  ---> simulating diminishing returns by making the benefit of upgrade rise logarithmically
    //    timeCoefficinet = Mathf.Log10(UpgradeLevel) + 1;

    //    return timeCoefficinet += (0.2f * UpgradeLevel);
    //}
}
