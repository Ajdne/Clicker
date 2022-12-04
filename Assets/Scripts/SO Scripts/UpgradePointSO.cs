using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Point SO", menuName = "Scriptable Objects/Upgrade Points")]
public class UpgradePointSO : UnlockableSO
{
    [SerializeField] private bool isUnlocked = false;
    public bool IsUnlocked { get { return isUnlocked; } set { isUnlocked = value; } }

    private int _upgradeLevel = 1;
    public int UpgradeLevel { get { return _upgradeLevel; } set { _upgradeLevel = value; } }

    [SerializeField] private float price = 10;
    public float Price { get { return price; } set { price = value; } }

    [SerializeField] private float timeCoefficinet = 1;
    public float TimeCoefficient { set { timeCoefficinet = value; } }

    public void Reset()
    {
        isUnlocked = false;
        price = 10;
        _upgradeLevel = 1;
    }

    public float UpgradeTimeCoefficient()
    {
        // f(x) = log(x)  ---> simulating diminishing returns by making the benefit of upgrade rise logarithmically
        timeCoefficinet = Mathf.Log10(UpgradeLevel) + 1;

        return timeCoefficinet *= (0.2f * UpgradeLevel);
    }
}
